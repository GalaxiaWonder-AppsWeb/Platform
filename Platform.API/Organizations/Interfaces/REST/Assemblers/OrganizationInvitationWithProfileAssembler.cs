using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

/// <summary>
/// Static assembler that converts an <see cref="OrganizationInvitation"/> and corresponding <see cref="ProfileDetails"/>
/// into a <see cref="OrganizationInvitationResource"/> for client-facing use.
/// </summary>
public static class OrganizationInvitationWithProfileAssembler
{
    /// <summary>
    /// Maps an <see cref="OrganizationInvitation"/> and a <see cref="ProfileDetails"/> instance into a 
    /// <see cref="OrganizationInvitationResource"/>.
    /// </summary>
    /// <param name="invitation">The domain entity representing the invitation.</param>
    /// <param name="profile">The profile information of the invited person.</param>
    /// <returns>A resource containing invitation and profile data.</returns>
    /// <exception cref="Exception">Thrown if the invitation's status is null, which indicates the status was not eagerly loaded.</exception>
    public static OrganizationInvitationResource ToResource(
        OrganizationInvitation invitation,
        ProfileDetails profile)
    {
        if (invitation.Status == null)
            throw new Exception("Invitation.Status is null. Make sure to include it in your query.");

        return new OrganizationInvitationResource(
            Id: invitation.Id,
            OrganizationId: invitation.OrganizationId.organizationId,
            PersonId: invitation.PersonId.personId,
            FirstName: profile.FirstName,
            LastName: profile.LastName,
            Email: profile.Email,
            Status: invitation.Status.Name.ToString(),
            CreatedAt: invitation.CreatedDate.Value
        );
    }
}
