namespace Platform.API.Organizations.Domain.Services;

public interface IOrganizationMemberQueryService
{
    Task<long?> GetPersonIdByOrganizationMemberId(long organizationMemberId);
}