using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

public interface IOrganizationRepository : IBaseRepository<Organization>
{
    bool ExistsByRuc(string ruc);
    
    Task<Organization?> FindByRucAsync(string ruc);

    Task<Organization?> FindOrganizationByMemberId(long memberId);
    
    Task<Organization?> FindOrganizationByInvitationId(long invitationId);
    
    Task<IEnumerable<Organization>> FindOrganizationsByOrganizationMemberPersonId(long memberPersonId);
}