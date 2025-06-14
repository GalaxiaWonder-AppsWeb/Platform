using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.IAM.Interfaces.ACL;

public interface IIAMContextFacade
{
    Task<ProfileDetails?> GetProfileDetailsByIdAsync(long personId);
    Task<List<ProfileDetails>> GetProfileDetailsByIdsAsync(IEnumerable<long> personIds);
}