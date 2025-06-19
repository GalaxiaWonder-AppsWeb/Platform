using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

/// <summary>
/// Static assembler responsible for converting domain entities related to an organization invitation 
/// into a resource representation for response.
/// </summary>
public static class InvitePersonToOrganizationAssembler
{
    /// <summary>
    /// Converts the given <see cref="Organization"/>, <see cref="OrganizationInvitation"/>, and <see cref="ProfileDetails"/>
    /// into an <see cref="InvitePersonToOrganizationResponse"/>.
    /// </summary>
    /// <param name="organization">The organization to which the person is being invited.</param>
    /// <param name="invitation">The invitation entity containing status and identifiers.</param>
    /// <param name="profile">The profile details of the invited person.</param>
    /// <returns>An <see cref="InvitePersonToOrganizationResponse"/> representing the invitation result.</returns>
    public static InvitePersonToOrganizationResponse ToResource(
        Organization organization,
        OrganizationInvitation invitation,
        ProfileDetails profile)
    {
        return new InvitePersonToOrganizationResponse(
            organization.Id,
            invitation.Id,
            invitation.Status.Name.ToString(),
            new ProfileDetailsResource(
                profile.Id,
                profile.FirstName,
                profile.LastName,
                profile.Email
            )
        );
    }
}