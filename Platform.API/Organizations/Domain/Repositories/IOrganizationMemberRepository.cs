using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

public interface IOrganizationMemberRepository: IBaseRepository<OrganizationMember>
{
    Task<IEnumerable<OrganizationMember>> FindMembersByOrganizationId(long organizationId);
}