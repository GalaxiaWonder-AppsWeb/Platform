using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public static class OrganizationMemberWithProfileAssembler
{
    public static OrganizationMemberResource ToResource(
        OrganizationMember member,
        ProfileDetails profile)
    {
        return new OrganizationMemberResource(
            member.Id,
            profile.FirstName,
            profile.LastName,
            profile.Email,
            member.MemberType.Name.ToString(),
            member.CreatedDate.Value
        );
    }
}