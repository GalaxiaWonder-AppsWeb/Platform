using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.IAM.Application.ACL;

public class IAMContextFacade(IPersonRepository personRepository) : IIAMContextFacade
{
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