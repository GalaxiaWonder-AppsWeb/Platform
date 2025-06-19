using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Services;

/// <summary>
/// Defines the contract for handling commands related to organization lifecycle and membership management.
/// </summary>
public interface IOrganizationCommandService
{
    /// <summary>
    /// Handles the creation of a new organization.
    /// </summary>
    /// <param name="command">The command containing the details to create the organization.</param>
    /// <returns>The newly created <see cref="Organization"/> entity.</returns>
    Task<Organization> Handle(CreateOrganizationCommand command);
    
    /// <summary>
    /// Handles the deletion of an existing organization.
    /// </summary>
    /// <param name="command">The command specifying the organization to delete.</param>
    Task Handle(DeleteOrganizationCommand command);
    
    
    /// <summary>
    /// Handles the update of an existing organization.
    /// </summary>
    /// <param name="command">The command containing the new values for the organization.</param>
    /// <returns>The updated <see cref="Organization"/> entity.</returns>
    Task<Organization> Handle(UpdateOrganizationCommand command);
    
    /// <summary>
    /// Handles the process of inviting a person to an organization by their email.
    /// </summary>
    /// <param name="command">The command containing the organization ID and the person's email.</param>
    /// <returns>A tuple containing the updated <see cref="Organization"/>, the created <see cref="OrganizationInvitation"/>, and the <see cref="ProfileDetails"/> of the invitee.</returns>
    Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(InvitePersonToOrganizationByEmailCommand command);
    
    
    /// <summary>
    /// Handles the acceptance of an invitation to join an organization.
    /// </summary>
    /// <param name="command">The command identifying the invitation to accept.</param>
    /// <returns>A tuple containing the updated <see cref="Organization"/>, the accepted <see cref="OrganizationInvitation"/>, and the <see cref="ProfileDetails"/> of the person accepting.</returns>
    Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(AcceptInvitationCommand command);
    
    /// <summary>
    /// Handles the rejection of an invitation to join an organization.
    /// </summary>
    /// <param name="command">The command identifying the invitation to reject.</param>
    /// <returns>A tuple containing the updated <see cref="Organization"/>, the rejected <see cref="OrganizationInvitation"/>, and the <see cref="ProfileDetails"/> of the person rejecting.</returns>
    Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(RejectInvitationCommand command);
    
    /// <summary>
    /// Handles the deletion of a member from an organization.
    /// </summary>
    /// <param name="command">The command specifying the member to delete.</param>
    Task Handle(DeleteOrganizationMemberCommand command);
}