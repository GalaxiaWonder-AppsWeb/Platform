using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.IAM.Interfaces.ACL;

/// <summary>
///     Facade interface for accessing identity and access management (IAM) profile information,
///     based on person identifiers or email.
/// </summary>
public interface IIAMContextFacade
{
    /// <summary>
    ///     Retrieves profile details for a person by their unique identifier.
    /// </summary>
    /// <param name="personId">The unique ID of the person.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="ProfileDetails"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ProfileDetails?> GetProfileDetailsByIdAsync(long personId);
    
    /// <summary>
    ///     Retrieves profile details for a person by their email address.
    /// </summary>
    /// <param name="email">The email address of the person.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="ProfileDetails"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ProfileDetails?> GetProfileDetailsByEmailAsync(string email);
    
    /// <summary>
    ///     Retrieves profile details for a list of person identifiers.
    /// </summary>
    /// <param name="personIds">A collection of person IDs to search for.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a list of <see cref="ProfileDetails"/> for the matching persons.
    /// </returns>
    Task<List<ProfileDetails>> GetProfileDetailsByIdsAsync(IEnumerable<long> personIds);
}