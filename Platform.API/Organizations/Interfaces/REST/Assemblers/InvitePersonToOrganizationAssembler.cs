using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public static class InvitePersonToOrganizationAssembler
{
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