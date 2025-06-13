using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;

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



        base.OnModelCreating(builder);
    }

}