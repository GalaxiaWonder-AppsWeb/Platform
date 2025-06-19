using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

public interface IOrganizationInvitationRepository: IBaseRepository<OrganizationInvitation>
{
    new Task<OrganizationInvitation?> FindByIdAsync(long id);
    Task<IEnumerable<OrganizationInvitation>> FindInvitationsByMemberPersonId(long id);

    Task<OrganizationInvitation?> FindLatestInvitation(long organizationId, long personId);

    Task<IEnumerable<OrganizationInvitation>> FindInvitationsByOrganizationId(long id);

    Task<IEnumerable<(OrganizationInvitation, Organization, Person)>> FindAllInvitationsWithDetailsByPersonIdAsync(long personId);
}