using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Repository implementation for managing <see cref="Person"/> entities.
/// </summary>
/// <param name="context">The application's database context.</param>
public class PersonRepository(AppDbContext context) : BaseRepository<Person>(context), IPersonRepository
{
    /// <summary>
    ///     Determines whether a person with the specified email address exists in the database.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns><c>true</c> if a matching person is found; otherwise, <c>false</c>.</returns>
    public bool ExistsByEmail(EmailAddress email)
    {
        return Context.Set<Person>().Any(person => person.Email.Address.Equals(email.Address));
    }
    
    /// <summary>
    ///     Determines whether a person with the specified phone number exists in the database.
    /// </summary>
    /// <param name="phone">The phone number to check.</param>
    /// <returns><c>true</c> if a matching person is found; otherwise, <c>false</c>.</returns>
    public bool ExistsByPhone(PhoneNumber phone)
    {
        return Context.Set<Person>().Any(person => person.Phone.Equals(phone));
    }
    
    /// <summary>
    ///     Asynchronously finds a person by their email address.
    /// </summary>
    /// <param name="email">The email address of the person to find.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="Person"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public new async Task<Person?> FindByEmailAsync(string email)
    {
        return await context.Persons
            .FirstOrDefaultAsync(p => p.Email.Address == email);
    }
}