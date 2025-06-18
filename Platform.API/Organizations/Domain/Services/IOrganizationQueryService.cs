using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Queries;

namespace Platform.API.Organizations.Domain.Services;

public interface IOrganizationQueryService
{
    Task<Organization?> Handle(GetOrganizationByIdQuery query);
    
    Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery query);

    Task<IEnumerable<Organization>> Handle(GetAllOrganizationsByMemberPersonIdQuery query);
}