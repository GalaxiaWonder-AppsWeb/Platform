using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;

namespace Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // PERSON
        builder.Entity<Person>(person =>
        {
            person.ToTable("persons");
            person.HasKey(p => p.Id);

            person.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            person.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("first_name").IsRequired();
                name.Property(n => n.LastName).HasColumnName("last_name").IsRequired();
            });

            person.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Address).HasColumnName("email_address").IsRequired();
            });

            person.OwnsOne(p => p.Phone, phone =>
            {
                phone.Property(p => p.Phone).HasColumnName("phone_number");
            });
        });

        // USER ACCOUNT
        builder.Entity<UserAccount>(user =>
        {
            user.ToTable("user_accounts");
            user.HasKey(u => u.Id);

            user.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            user.OwnsOne(u => u.Username, uname =>
            {
                uname.Property(u => u.Username).HasColumnName("username").IsRequired();
            });

            user.OwnsOne(u => u.PasswordHash, pwd =>
            {
                pwd.Property(p => p.HashedPassword).HasColumnName("password_hash").IsRequired();
            });

            user.Property(u => u.UserType)
                .HasColumnName("user_type")
                .IsRequired()
                .HasConversion<string>(); // Enum como string

            user.Ignore(u => u.PersonId);

            user.Property<long>("person_id");

            user.HasOne<Person>()
                .WithOne()
                .HasPrincipalKey<Person>(p => p.Id)
                .HasForeignKey<UserAccount>(u => u.PersonIdValue)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(builder);
    }

}