using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public static class OrganizationInvitationWithDetailsAssembler
{
    public static OrganizationInvitationWithDetailsResource ToResource(
        OrganizationInvitation invitation,
        Organization organization,
        Person invitedBy)
    {
        return new OrganizationInvitationWithDetailsResource
        {
            Id = invitation.Id,
            OrganizationName = organization.LegalName.Name,
            InvitedByFullName = $"{invitedBy.Fullname}",
            InvitedByEmail = invitedBy.Email.Address,
            Status = invitation.Status.Name.ToString()
        };
    }
}
