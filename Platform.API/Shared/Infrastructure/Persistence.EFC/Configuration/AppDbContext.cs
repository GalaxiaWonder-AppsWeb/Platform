using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;

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

            entity.OwnsOne(m => m.OrganizationId, org =>
            {
                org.Property(p => p.organizationId)
                    .HasColumnName("organization_id")
                    .IsRequired();
            });

            entity.OwnsOne(m => m.PersonId, person =>
            {
                person.Property(p => p.personId)
                    .HasColumnName("person_id")
                    .IsRequired();
            });

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
            new { Id = 1L, Name = OrganizationInvitationStatuses.PENDING },
            new { Id = 2L, Name = OrganizationInvitationStatuses.ACCEPTED },
            new { Id = 3L, Name = OrganizationInvitationStatuses.REJECTED }
        );

        
        base.OnModelCreating(builder);
    }

}