using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;

namespace Platform.API.Organizations.Application.Internal.QueryServices;

public class OrganizationQueryService(IOrganizationRepository organizationRepository) : IOrganizationQueryService
{
    public async Task<Organization?> Handle(GetOrganizationByIdQuery query)
    {
        return await organizationRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery query)
    {
        return await organizationRepository.ListAsync();
    }
    
    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsByMemberPersonIdQuery query)
    {
        return await organizationRepository.FindOrganizationsByOrganizationMemberPersonId(query.Id);
    }
}