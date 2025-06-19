using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

/// <summary>
/// Static assembler responsible for transforming domain entities related to an organization invitation 
/// into a detailed resource representation.
/// </summary>
public static class OrganizationInvitationWithDetailsAssembler
{
    /// <summary>
    /// Converts an <see cref="OrganizationInvitation"/>, its associated <see cref="Organization"/>, 
    /// and the <see cref="Person"/> who sent the invitation into a <see cref="OrganizationInvitationWithDetailsResource"/>.
    /// </summary>
    /// <param name="invitation">The organization invitation entity.</param>
    /// <param name="organization">The organization to which the invitation belongs.</param>
    /// <param name="invitedBy">The person who sent the invitation.</param>
    /// <returns>A <see cref="OrganizationInvitationWithDetailsResource"/> containing detailed invitation data.</returns>
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
            Status = invitation.Status.Name.ToString(),
            InvitedOn = invitation.CreatedDate.Value
        };
    }
}
