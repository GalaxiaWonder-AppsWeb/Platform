using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

/// <summary>
/// Static assembler responsible for converting a domain <see cref="OrganizationMember"/> entity and its associated
/// <see cref="ProfileDetails"/> into a client-facing <see cref="OrganizationMemberResource"/>.
/// </summary>
public static class OrganizationMemberWithProfileAssembler
{
    /// <summary>
    /// Maps an <see cref="OrganizationMember"/> and its associated <see cref="ProfileDetails"/> into a
    /// <see cref="OrganizationMemberResource"/> DTO.
    /// </summary>
    /// <param name="member">The domain entity representing the organization member.</param>
    /// <param name="profile">The profile information of the member (e.g., first name, last name, email).</param>
    /// <returns>A resource DTO that aggregates member and profile information for presentation layers.</returns>
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