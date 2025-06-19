using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Services;

public interface IOrganizationQueryService
{
    Task<Organization?> Handle(GetOrganizationByIdQuery query);
    Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery query);
    Task<IEnumerable<Organization>> Handle(GetAllOrganizationsByMemberPersonIdQuery query);
    Task<IEnumerable<(OrganizationInvitation, ProfileDetails)>> Handle(GetAllInvitationsByOrganizationIdQuery query);
    Task<IEnumerable<(OrganizationMember, ProfileDetails)>> Handle(GetAllMembersByOrganizationIdQuery query);
    Task<IEnumerable<(OrganizationInvitation, Organization, Person)>> Handle(GetAllInvitationsByPersonIdQuery query);
}