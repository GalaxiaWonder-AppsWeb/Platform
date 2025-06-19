using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Services;

/// <summary>
/// Defines the contract for handling queries related to organizations, their members, and invitations.
/// </summary>
public interface IOrganizationQueryService
{
    /// <summary>
    /// Retrieves a specific organization by its ID.
    /// </summary>
    /// <param name="query">The query containing the ID of the organization.</param>
    /// <returns>The matching <see cref="Organization"/> if found, or <c>null</c> otherwise.</returns>
    Task<Organization?> Handle(GetOrganizationByIdQuery query);
    
    /// <summary>
    /// Retrieves all organizations in the system.
    /// </summary>
    /// <param name="query">The query object (empty in this case).</param>
    /// <returns>A collection of all <see cref="Organization"/> instances.</returns>
    Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery query);
    
    /// <summary>
    /// Retrieves all organizations in which the given person is registered as a member.
    /// </summary>
    /// <param name="query">The query containing the person ID.</param>
    /// <returns>A collection of <see cref="Organization"/> instances where the person is a member.</returns>
    Task<IEnumerable<Organization>> Handle(GetAllOrganizationsByMemberPersonIdQuery query);
    
    /// <summary>
    /// Retrieves all invitations for a specific organization, including details of the invited persons.
    /// </summary>
    /// <param name="query">The query containing the organization ID.</param>
    /// <returns>A collection of tuples with each <see cref="OrganizationInvitation"/> and its corresponding <see cref="ProfileDetails"/>.</returns>
    Task<IEnumerable<(OrganizationInvitation, ProfileDetails)>> Handle(GetAllInvitationsByOrganizationIdQuery query);
    
    /// <summary>
    /// Retrieves all members of a specific organization, including their profile details.
    /// </summary>
    /// <param name="query">The query containing the organization ID.</param>
    /// <returns>A collection of tuples with each <see cref="OrganizationMember"/> and its corresponding <see cref="ProfileDetails"/>.</returns>
    Task<IEnumerable<(OrganizationMember, ProfileDetails)>> Handle(GetAllMembersByOrganizationIdQuery query);
    
    /// <summary>
    /// Retrieves all invitations received by a specific person, including the related organization and inviter person details.
    /// </summary>
    /// <param name="query">The query containing the person ID.</param>
    /// <returns>A collection of tuples with each <see cref="OrganizationInvitation"/>, the related <see cref="Organization"/>, and the <see cref="Person"/> who sent the invitation.</returns>
    Task<IEnumerable<(OrganizationInvitation, Organization, Person)>> Handle(GetAllInvitationsByPersonIdQuery query);
}