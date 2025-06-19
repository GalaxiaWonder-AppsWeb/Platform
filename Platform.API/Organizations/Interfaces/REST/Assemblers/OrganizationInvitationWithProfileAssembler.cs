using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public static class OrganizationInvitationWithProfileAssembler
{
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
