using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Model.Events;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;
using TaskStatus = Platform.API.Projects.Domain.Model.Entities.TaskStatus;

namespace Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    //IAM CONTEXT
    public DbSet<UserAccount> UserAccounts { get; set; } = null!;
    public DbSet<UserType> UserTypes { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;
    // ORGANIZATION CONTEXT
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationMember> OrganizationMembers { get; set; }
    public DbSet<OrganizationInvitation> OrganizationInvitations { get; set; }
    
    //PROJECT CONTEXT
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ProjectStatus> ProjectStatuss { get; set; } = null!;
    public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        
        //PARA IGNORAR EVENTOS DE DOMINIO
        builder.Ignore<DomainEvent>();

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

            user.Property(u => u.Id).ValueGeneratedOnAdd();

            user.OwnsOne(u => u.Username, uname =>
            {
                uname.Property(u => u.Username).HasColumnName("username").IsRequired();
            });

            user.OwnsOne(u => u.PasswordHash, pwd =>
            {
                pwd.Property(p => p.HashedPassword).HasColumnName("password_hash").IsRequired();
            });

            user.HasOne(u => u.UserType)
                .WithMany()
                .HasForeignKey("user_type_id")
                .OnDelete(DeleteBehavior.Restrict);

            user.OwnsOne(u => u.PersonId, pid =>
            {
                pid.Property(p => p.personId)
                    .HasColumnName("person_id")
                    .IsRequired();
            });

            user.HasOne<Person>()
                .WithOne()
                .HasForeignKey<UserAccount>("person_id")
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        //USER TYPES
        builder.Entity<UserType>(userType =>
        {
            userType.ToTable("user_types");
            userType.HasKey(ut => ut.Id);
            userType.Property(ut => ut.Id).ValueGeneratedOnAdd();

            userType.Property(ut => ut.Name)
                .HasConversion<string>()
                .IsRequired();
        });
        
        // ORGANIZATIONS
        builder.Entity<Organization>(entity =>
        {
            entity.ToTable("organizations");

            entity.HasKey(o => o.Id);

            entity.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.OwnsOne(o => o.LegalName, ln =>
            {
                ln.Property(p => p.Name)
                    .HasColumnName("legal_name")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            entity.OwnsOne(o => o.CommercialName, cn =>
            {
                cn.Property(p => p.Name)
                    .HasColumnName("commercial_name")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            entity.OwnsOne(o => o.Ruc, r =>
            {
                r.Property(p => p.Number)
                    .HasColumnName("ruc")
                    .HasMaxLength(11)
                    .IsRequired();
            });

            entity.OwnsOne(o => o.CreatedBy, cb =>
            {
                cb.Property(p => p.personId)
                    .HasColumnName("created_by")
                    .IsRequired();
            });

            //enum
            builder.Entity<Organization>(org =>
            {
                org.Property(s=>s.OrganizationStatusId)
                    .HasColumnName("status")
                    .IsRequired();
                
                org.HasOne(o => o.Status)
                    .WithMany()
                    .HasForeignKey(o => o.OrganizationStatusId)
                    .IsRequired();
            });


            entity.Ignore(o => o.OrganizationMemberIds);
            entity.Ignore(o => o.OrganizationInvitationIds);
        });

        //ORGANIZATION MEMBER
        builder.Entity<OrganizationMember>(entity =>
        {
            entity.ToTable("organization_members");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            // Value Object: OrganizationId
            entity.OwnsOne(m => m.OrganizationId, org =>
            {
                org.Property(p => p.organizationId)
                    .HasColumnName("organization_id")
                    .IsRequired();
            });

            // Value Object: PersonId
            entity.OwnsOne(m => m.PersonId, person =>
            {
                person.Property(p => p.personId)
                    .HasColumnName("person_id")
                    .IsRequired();
            });

            // FK: MemberType
            entity.Property(m => m.MemberTypeId)
                .HasColumnName("type")
                .IsRequired();

            entity.HasOne(m => m.MemberType)
                .WithMany()
                .HasForeignKey(m => m.MemberTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        //ORGANIZATION INVITATION
        builder.Entity<OrganizationInvitation>(entity =>
        {
            entity.ToTable("organization_invitations");

            entity.HasKey(i => i.Id);

            entity.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.OwnsOne(i => i.OrganizationId, org =>
            {
                org.Property(p => p.organizationId)
                    .HasColumnName("organization_id")
                    .IsRequired();
            });

            entity.OwnsOne(i => i.PersonId, person =>
            {
                person.Property(p => p.personId)
                    .HasColumnName("person_id")
                    .IsRequired();
            });

            entity.OwnsOne(i => i.InvitedBy, invited =>
            {
                invited.Property(p => p.personId)
                    .HasColumnName("invited_by")
                    .IsRequired();
            });

            entity.Property(i => i.OrganizationInvitationStatusId)
                .HasColumnName("status")
                .IsRequired();

            entity.HasOne(i => i.Status)
                .WithMany()
                .HasForeignKey(i => i.OrganizationInvitationStatusId)
                .OnDelete(DeleteBehavior.Restrict);

        });
        
        //ORGANIZATION STATUS
        builder.Entity<OrganizationStatus>(entity =>
        {
            entity.ToTable("organization_statuses");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(s => s.Name)
                .HasColumnName("name")
                .HasConversion<string>()
                .IsRequired();
        });
        
        //ORGANIZATION MEMBER TYPE
        builder.Entity<OrganizationMemberType>(entity =>
        {
            entity.ToTable("organization_member_types");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(t => t.Name)
                .HasColumnName("name")
                .HasConversion<string>()
                .IsRequired();
        });

        //ORGANIZATION INVITATION STATUS
        builder.Entity<OrganizationInvitationStatus>(entity =>
        {
            entity.ToTable("organization_invitation_statuses");

            entity.HasKey(i => i.Id);

            entity.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(i => i.Name)
                .HasColumnName("name")
                .HasConversion<string>()
                .IsRequired();
        });
        
        // PROJECT
        builder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.OwnsOne(p => p.ProjectName, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("name")
                    .IsRequired();
            });

            entity.OwnsOne(p => p.Description, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("description");
            });
            
            entity.OwnsOne(p => p.DateRange, range =>
            {
                range.Property(r => r.StartDate)
                    .HasColumnName("starting_date")
                    .IsRequired();
                range.Property(r => r.EndDate)
                    .HasColumnName("ending_date")
                    .IsRequired();
            });
            

            entity.OwnsOne(p => p.OrganizationId, owned =>
            {
                owned.Property(o => o.organizationId)
                    .HasColumnName("organization_id")
                    .IsRequired();
            });
            

            entity.OwnsOne(p => p.ContractingEntityId, owned =>
            {
                owned.Property(o => o.personId)
                    .HasColumnName("contracting_entity_id")
                    .IsRequired();
            });

            entity.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(p => p.StatusId)
                .HasColumnName("status_id");
            
            entity.OwnsOne(p => p.Budget, budget =>
            {
                budget.Property(b => b.Amount)
                    .HasColumnName("budget")
                    .IsRequired();
                budget.Property(b => b.Currency)
                    .HasColumnName("budget_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });
        
        //PROJECT STATUS
        builder.Entity<ProjectStatus>(entity =>
        {
            entity.ToTable("project_statuses");

            entity.HasKey(i => i.Id);

            entity.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(i => i.Name)
                .HasColumnName("name")
                .HasConversion<string>()
                .IsRequired();
        });
        
        //PROJECT TEAM MEMBER
        builder.Entity<ProjectTeamMember>(entity =>
        {
            entity.ToTable("project_team_members");

            entity.HasKey(ptm => ptm.Id);

            entity.Property(ptm => ptm.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.OwnsOne(ptm => ptm.ProjectId, proj =>
            {
                proj.Property(p => p.Value)
                    .HasColumnName("project_id")
                    .IsRequired();
            });
            
            // FK: MemberType
            entity.Property(m => m.SpecialtyId)
                .HasColumnName("specialty_id")
                .IsRequired();

            entity.HasOne(m => m.Specialty)
                .WithMany()
                .HasForeignKey(m => m.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            entity.OwnsOne(ptm => ptm.OrganizationMemberId, member =>
            {
                member.Property(m => m.organizationMemberId)
                    .HasColumnName("organization_member_id")
                    .IsRequired();
            });
            
            entity.OwnsOne(ptm => ptm.PersonId, member =>
            {
                member.Property(m => m.personId)
                    .HasColumnName("person_id")
                    .IsRequired();
            });
            
            entity.OwnsOne(p => p.PersonName, desc =>
            {
                desc.Property(d => d.FirstName)
                    .HasColumnName("first_name");
            });
            
            entity.OwnsOne(p => p.PersonName, desc =>
            {
                desc.Property(d => d.LastName)
                    .HasColumnName("last_name");
            });
            
            
            entity.OwnsOne(p => p.EmailAddress, desc =>
            {
                desc.Property(d => d.Address)
                    .HasColumnName("email_address");
            });
        });
        
        //SPECIALTY
        builder.Entity<Specialty>(entity =>
        {
            entity.ToTable("specialties");

            entity.HasKey(i => i.Id);

            entity.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(i => i.Name)
                .HasColumnName("name")
                .HasConversion<string>()
                .IsRequired();
        });
        
        // MILESTONE
        builder.Entity<Milestone>(entity =>
        {
            entity.ToTable("milestones");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("name")
                    .IsRequired();
            });

            entity.OwnsOne(p => p.Description, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("description");
            });

            entity.OwnsOne(p => p.ProjectId, owned =>
            {
                owned.Property(o => o.Value)
                    .HasColumnName("project_id")
                    .IsRequired();
            });
            
            entity.OwnsOne(p => p.DateRange, range =>
            {
                range.Property(r => r.StartDate)
                    .HasColumnName("starting_date")
                    .IsRequired();
                range.Property(r => r.EndDate)
                    .HasColumnName("ending_date")
                    .IsRequired();
            });
        });
        
        // TASK
        builder.Entity<Task>(entity =>
        {
            entity.ToTable("tasks");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("name")
                    .IsRequired();
            });

            entity.OwnsOne(p => p.Description, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("description");
            });
            
            entity.OwnsOne(p => p.DateRange, range =>
            {
                range.Property(r => r.StartDate)
                    .HasColumnName("starting_date")
                    .IsRequired();
                range.Property(r => r.EndDate)
                    .HasColumnName("ending_date")
                    .IsRequired();
            });
            
            entity.OwnsOne(p => p.MilestoneId, owned =>
            {
                owned.Property(o => o.Value)
                    .HasColumnName("milestone_id")
                    .IsRequired();
            });
            
            entity.OwnsOne(p => p.PersonId, person =>
            {
                person.Property(p => p.personId)
                    .HasColumnName("person_id")
                    .IsRequired();
            });
            
            entity.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Specialty)
                .WithMany()
                .HasForeignKey(p => p.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(p => p.StatusId)
                .HasColumnName("status_id");

            entity.Property(p => p.SpecialtyId)
                .HasColumnName("specialty_id");

        });
        
        //TASK STATUS
        builder.Entity<TaskStatus>(entity =>
        {
            entity.ToTable("task_statuses");

            entity.HasKey(i => i.Id);

            entity.Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(i => i.Name)
                .HasColumnName("name")
                .HasConversion<string>()
                .IsRequired();
        });
        
        //SETTEO DE DATA
        builder.Entity<OrganizationStatus>().HasData(
            new { Id = 1L, Name = OrganizationStatuses.ACTIVE },
            new { Id = 2L, Name = OrganizationStatuses.INACTIVE }
        );

        builder.Entity<OrganizationMemberType>().HasData(
            new { Id = 1L, Name = OrganizationMemberTypes.CONTRACTOR },
            new { Id = 2L, Name = OrganizationMemberTypes.WORKER }
        );

       builder.Entity<OrganizationInvitationStatus>().HasData(
            new { Id = 1L, Name = OrganizationInvitationStatuses.PENDING},
            new { Id = 2L, Name = OrganizationInvitationStatuses.ACCEPTED },
            new { Id = 3L, Name = OrganizationInvitationStatuses.REJECTED }
        );
       
       builder.Entity<ProjectStatus>().HasData(
            new { Id = 1L, Name = ProjectStatuses.BASIC_STUDIES },
            new { Id = 2L, Name = ProjectStatuses.DESIGN_IN_PROCESS },
            new { Id = 3L, Name = ProjectStatuses.UNDER_REVIEW },
            new { Id = 4L, Name = ProjectStatuses.CHANGE_REQUESTED },
            new { Id = 5L, Name = ProjectStatuses.CHANGE_PENDING },
            new { Id = 6L, Name = ProjectStatuses.APPROVED }
        );
       
       builder.Entity<Specialty>().HasData(
            new { Id = 1L, Name = Specialties.ARCHITECTURE },
            new { Id = 2L, Name = Specialties.STRUCTURES },
            new { Id = 3L, Name = Specialties.HSA },
            new { Id = 4L, Name = Specialties.TOPOGRAPHY },
            new { Id = 5L, Name = Specialties.SANITATION },
            new { Id = 6L, Name = Specialties.ELECTRICITY },
            new { Id = 7L, Name = Specialties.COMMUNICATIONS },
            new { Id = 8L, Name = Specialties.NON_APPLICABLE }
        );
       
       builder.Entity<TaskStatus>().HasData(
           new { Id = 1L, Name = TaskStatuses.DRAFT },
           new { Id = 2L, Name = TaskStatuses.PENDING },
           new { Id = 3L, Name = TaskStatuses.SUBMITTED },
           new { Id = 4L, Name = TaskStatuses.APPROVED },
           new { Id = 5L, Name = TaskStatuses.REJECTED }
        );

    }

}