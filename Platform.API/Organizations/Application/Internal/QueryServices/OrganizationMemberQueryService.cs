using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;

namespace Platform.API.Organizations.Application.Internal.QueryServices;

public class OrganizationMemberQueryService(
    IOrganizationMemberRepository organizationMemberRepository): IOrganizationMemberQueryService
{
    public async Task<long?> GetPersonIdByOrganizationMemberId(long organizationMemberId)
    {
        return await organizationMemberRepository.FindPersonIdByOrganizationMemberId(organizationMemberId);
    }
}