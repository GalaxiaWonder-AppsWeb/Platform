using Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Platform.API.IAM.Application.ACL;
using Platform.API.IAM.Application.Internal.CommandServices;
using Platform.API.IAM.Application.Internal.OutboundServices;
using Platform.API.IAM.Application.Internal.QueryServices;
using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Platform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Platform.API.IAM.Infrastructure.Tokens.JWT.Services;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Organizations.Application.Internal.CommandServices;
using Platform.API.Organizations.Application.Internal.QueryServices;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;
using Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.Projects.Application.Internal.CommandServices;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.Shared.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "PropGMS.Platform.API",
            Version = "v1",
            Description = "PropGMS Platform API",
            TermsOfService = new Uri("https://propgms.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "PropGMS",
                Email = "contact@acme.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0")
            },
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonQueryService, PersonQueryService > ();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<IUserTypeCommandService, UserTypeCommandService>();
builder.Services.AddScoped<IUserTypeQueryService, UserTypeQueryService>();
builder.Services.AddScoped<IIAMContextFacade, IAMContextFacade>();


// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserAccountCommandService, UserAccountCommandService>();
builder.Services.AddScoped<IUserAccountQueryService, UserAccountQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// Organization Configuration
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();
builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
builder.Services.AddScoped<IOrganizationStatusRepository, OrganizationStatusRepository>();
builder.Services.AddScoped<IOrganizationMemberRepository, OrganizationMemberRepository>();
builder.Services.AddScoped<IOrganizationMemberTypeRepository, OrganizationMemberTypeRepository>();
builder.Services.AddScoped<IOrganizationInvitationRepository, OrganizationInvitationRepository>();
builder.Services.AddScoped<IOrganizationInvitationStatusRepository, OrganizationInvitationStatusRepository>();

//Project Configuration
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
builder.Services.AddScoped<IProjectCommandService, ProjectCommandService>();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", 
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    await context.Database.EnsureCreatedAsync();

    var commandService = services.GetRequiredService<IUserTypeCommandService>();
    await commandService.Handle(new SeedUserTypeCommand());
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();