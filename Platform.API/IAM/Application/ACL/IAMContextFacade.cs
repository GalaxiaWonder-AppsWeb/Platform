using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.IAM.Application.ACL;

/// <summary>
///     Facade providing identity and access management (IAM) context operations,
///     such as retrieving profile details from person-related data.
/// </summary>
/// <param name="personRepository">The person repository used to retrieve person data.</param>
public class IAMContextFacade(IPersonRepository personRepository) : IIAMContextFacade
{
    /// <summary>
    ///     Retrieves profile details of a person by their unique identifier.
    /// </summary>
    /// <param name="personId">The ID of the person.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="ProfileDetails"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ProfileDetails?> GetProfileDetailsByIdAsync(long personId)
    {
        var person = await personRepository.FindByIdAsync(personId);
        if (person is null) return null;

        return new ProfileDetails(
            person.Id,
            person.Name.FirstName,
            person.Name.LastName,
            person.Email.Address
        );
    }

    /// <summary>
    ///     Retrieves profile details of a person by their email address.
    /// </summary>
    /// <param name="email">The email address of the person.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="ProfileDetails"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ProfileDetails?> GetProfileDetailsByEmailAsync(string email)
    {
        var person = await personRepository.FindByEmailAsync(email);
        if (person is null) return null;

        return new ProfileDetails(
            person.Id,
            person.Name.FirstName,
            person.Name.LastName,
            person.Email.Address);
    }

    /// <summary>
    ///     Retrieves profile details for a list of person identifiers.
    /// </summary>
    /// <param name="personIds">A collection of person IDs.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a list of <see cref="ProfileDetails"/> for the matching persons.
    /// </returns>
    public async Task<List<ProfileDetails>> GetProfileDetailsByIdsAsync(IEnumerable<long> personIds)
    {
        var result = new List<ProfileDetails>();

        foreach (var id in personIds)
        {
            var person = await personRepository.FindByIdAsync(id);
            if (person != null)
            {
                result.Add(new ProfileDetails(
                    person.Id,
                    person.Name.FirstName,
                    person.Name.LastName,
                    person.Email.Address
                ));
            }
        }

        return result;
    }
}