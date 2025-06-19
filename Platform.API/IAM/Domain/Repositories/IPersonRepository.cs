using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

/// <summary>
///     Repository interface for managing <see cref="Person"/> entities.
/// </summary>
public interface IPersonRepository : IBaseRepository<Person>
{
    /// <summary>
    ///     Determines whether a person with the specified email address exists in the system.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns><c>true</c> if a person with the given email exists; otherwise, <c>false</c>.</returns>
    bool ExistsByEmail(EmailAddress email);
    
    /// <summary>
    ///     Determines whether a person with the specified phone number exists in the system.
    /// </summary>
    /// <param name="phone">The phone number to check.</param>
    /// <returns><c>true</c> if a person with the given phone number exists; otherwise, <c>false</c>.</returns>
    bool ExistsByPhone(PhoneNumber phone);
    
    /// <summary>
    ///     Asynchronously finds a person by their email address.
    /// </summary>
    /// <param name="email">The email address of the person to find.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <see cref="Person"/> if found; otherwise, <c>null</c>.
    /// </returns>
    new Task<Person?> FindByEmailAsync(string email);
}