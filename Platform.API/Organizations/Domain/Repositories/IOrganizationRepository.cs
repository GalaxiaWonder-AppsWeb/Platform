using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

public interface IOrganizationRepository : IBaseRepository<Organization>
{
    new Task<Organization?> FindByIdAsync(long id);
    
    bool ExistsByRuc(string ruc);
    
    bool ExistsById(long id);
    
    Task<Organization?> FindByRucAsync(string ruc);

    Task<Organization?> FindOrganizationByMemberId(long memberId);
    
    Task<Organization?> FindOrganizationByInvitationId(long invitationId);
    
    Task<IEnumerable<Organization>> FindOrganizationsByOrganizationMemberPersonId(long memberPersonId);
}