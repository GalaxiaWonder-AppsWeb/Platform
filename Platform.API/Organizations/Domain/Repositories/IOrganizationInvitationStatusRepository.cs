using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

public interface IOrganizationInvitationStatusRepository: IBaseRepository<OrganizationInvitationStatus>
{
    Task<OrganizationInvitationStatus?> FindByNameAsync(string name);
}