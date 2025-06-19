using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="OrganizationInvitation"/> entities.
/// </summary>
public interface IOrganizationInvitationRepository: IBaseRepository<OrganizationInvitation>
{
    /// <summary>
    /// Retrieves an <see cref="OrganizationInvitation"/> by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the invitation.</param>
    /// <returns>The matching invitation, or <c>null</c> if not found.</returns>
    new Task<OrganizationInvitation?> FindByIdAsync(long id);
    
    /// <summary>
    /// Retrieves all invitations sent to a person across any organization.
    /// </summary>
    /// <param name="id">The ID of the person receiving the invitations.</param>
    /// <returns>A collection of invitations for the specified person.</returns>
    Task<IEnumerable<OrganizationInvitation>> FindInvitationsByMemberPersonId(long id);
    
    /// <summary>
    /// Retrieves the most recent invitation sent to a person for a specific organization.
    /// </summary>
    /// <param name="organizationId">The ID of the organization sending the invitation.</param>
    /// <param name="personId">The ID of the person receiving the invitation.</param>
    /// <returns>The latest invitation, or <c>null</c> if none exists.</returns>
    Task<OrganizationInvitation?> FindLatestInvitation(long organizationId, long personId);

    
    /// <summary>
    /// Retrieves all invitations associated with a specific organization.
    /// </summary>
    /// <param name="id">The ID of the organization.</param>
    /// <returns>A collection of invitations sent by the organization.</returns>
    Task<IEnumerable<OrganizationInvitation>> FindInvitationsByOrganizationId(long id);

    /// <summary>
    /// Retrieves all invitations for a person, including organization and inviter details.
    /// </summary>
    /// <param name="personId">The ID of the person receiving the invitations.</param>
    /// <returns>
    /// A collection of tuples containing the invitation, the organization that sent it,
    /// and the person who initiated the invitation.
    /// </returns>
    Task<IEnumerable<(OrganizationInvitation, Organization, Person)>> FindAllInvitationsWithDetailsByPersonIdAsync(long personId);
}