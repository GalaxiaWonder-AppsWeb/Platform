using System.ComponentModel.DataAnnotations.Schema;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

/// <summary>
///     Entity representing an invitation for a person to join an organization.
///     Includes the status of the invitation, who sent it, and to whom it was sent.
/// </summary>
public partial class OrganizationInvitation
{
    /// <summary>
    ///     Gets the unique identifier of the invitation.
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    ///     Gets the ID of the organization to which the person is being invited.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; }

    /// <summary>
    ///     Gets the ID of the person being invited.
    /// </summary>
    public PersonId PersonId { get; private set; }

    
    /// <summary>
    ///     Gets the ID of the person who sent the invitation.
    /// </summary>
    public PersonId InvitedBy { get; private set; }

    /// <summary>
    ///     Gets the current status of the invitation.
    /// </summary>
    public OrganizationInvitationStatus Status { get; private set; }

    /// <summary>
    ///     Gets the ID of the invitation status (used as auxiliary field for persistence).
    /// </summary>
    [NotMapped]
    public long OrganizationInvitationStatusId { get; private set; }
    
    /// <summary>
    ///     Initializes a new empty instance of <see cref="OrganizationInvitation"/>. Required by EF Core.
    /// </summary>
    public OrganizationInvitation()
    {
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="OrganizationInvitation"/> with full information.
    /// </summary>
    /// <param name="organizationId">The ID of the organization issuing the invitation.</param>
    /// <param name="personId">The ID of the person being invited.</param>
    /// <param name="invitedBy">The ID of the person who sent the invitation.</param>
    /// <param name="status">The initial status of the invitation.</param>
    public OrganizationInvitation(OrganizationId organizationId, PersonId personId, PersonId invitedBy, OrganizationInvitationStatus status)
    {
        OrganizationId = organizationId;
        PersonId = personId;
        Status = status;
        InvitedBy = invitedBy;
    }

    /// <summary>
    ///     Changes the status of the invitation.
    /// </summary>
    /// <param name="status">The new status to assign to the invitation.</param>
    /// <exception cref="InvalidOperationException">Thrown if the invitation has already been answered.</exception>
    public void ChangeStatus(OrganizationInvitationStatus status)
    {
        if (Status.Name != OrganizationInvitationStatuses.PENDING)
            throw new InvalidOperationException("The invitation has already been answered.");

        Status = status;
    }

    /// <summary>
    ///     Sets the organization ID for the invitation.
    /// </summary>
    /// <param name="organizationId">The organization ID to assign.</param>
    public void SetOrganization(OrganizationId organizationId)
    {
        OrganizationId = organizationId;
    }
    
    /// <summary>
    ///     Sets the person who invited the user.
    /// </summary>
    /// <param name="personId">The ID of the person who sent the invitation.</param>
    public void SetInvitedBy(PersonId personId)
    {
        InvitedBy = personId;
    }
}