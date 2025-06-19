using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Application.Internal.QueryServices;

/// <summary>
/// Service responsible for handling query operations related to organizations, their members, and invitations.
/// </summary>
public class OrganizationQueryService(
    IOrganizationRepository organizationRepository,
    IOrganizationInvitationRepository organizationInvitationRepository,
    IIAMContextFacade iamContext,
    IOrganizationMemberRepository organizationMemberRepository
    ) : IOrganizationQueryService
{
    /// <summary>
    /// Retrieves an organization by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the organization ID.</param>
    /// <returns>The organization if found; otherwise, null.</returns>
    public async Task<Organization?> Handle(GetOrganizationByIdQuery query)
    {
        return await organizationRepository.FindByIdAsync(query.Id);
    }
    
    /// <summary>
    /// Retrieves a list of all registered organizations.
    /// </summary>
    /// <param name="query">The query with no filtering parameters.</param>
    /// <returns>A list of all organizations.</returns>
    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery query)
    {
        return await organizationRepository.ListAsync();
    }
    
    /// <summary>
    /// Retrieves all organizations in which a person is registered as a member.
    /// </summary>
    /// <param name="query">The query containing the person's ID.</param>
    /// <returns>A list of organizations the person belongs to.</returns>
    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsByMemberPersonIdQuery query)
    {
        return await organizationRepository.FindOrganizationsByOrganizationMemberPersonId(query.Id);
    }

    /// <summary>
    /// Retrieves all invitations sent by an organization along with profile details of invitees.
    /// </summary>
    /// <param name="query">The query containing the organization ID.</param>
    /// <returns>A list of tuples containing invitations and the corresponding profile details.</returns>
    /// <exception cref="Exception">Thrown when a profile could not be retrieved.</exception>
    public async Task<IEnumerable<(OrganizationInvitation, ProfileDetails)>> Handle(GetAllInvitationsByOrganizationIdQuery query)
    {
        var invitations = await organizationInvitationRepository.FindInvitationsByOrganizationId(query.Id);

        var results = new List<(OrganizationInvitation, ProfileDetails)>();

        foreach (var invitation in invitations)
        {
            var profile = await iamContext.GetProfileDetailsByIdAsync(invitation.PersonId.personId)
                          ?? throw new Exception($"Profile not found for person ID {invitation.PersonId.personId}");

            results.Add((invitation, profile));
        }
        return results;
    }
    
    /// <summary>
    /// Retrieves all members of an organization along with their profile details.
    /// </summary>
    /// <param name="query">The query containing the organization ID.</param>
    /// <returns>A list of tuples containing members and their associated profile details.</returns>
    /// <exception cref="Exception">Thrown when a profile could not be retrieved.</exception>
    public async Task<IEnumerable<(OrganizationMember, ProfileDetails)>> Handle(GetAllMembersByOrganizationIdQuery query)
    {
        var members = await organizationMemberRepository.FindMembersByOrganizationId(query.Id);

        var results = new List<(OrganizationMember, ProfileDetails)>();

        foreach (var member in members)
        {
            var profile = await iamContext.GetProfileDetailsByIdAsync(member.PersonId.personId)
                          ?? throw new Exception($"Profile not found for person ID {member.PersonId.personId}");

            results.Add((member, profile));
        }

        return results;
    }

    /// <summary>
    /// Retrieves all invitations addressed to a given person, including the organization and the inviter.
    /// </summary>
    /// <param name="query">The query containing the person's ID.</param>
    /// <returns>A list of tuples with the invitation, the organization, and the person who sent the invitation.</returns>
    public async Task<IEnumerable<(OrganizationInvitation, Organization, Person)>> Handle(GetAllInvitationsByPersonIdQuery query)
    {
        var result = await organizationInvitationRepository.FindAllInvitationsWithDetailsByPersonIdAsync(query.PersonId);

        return result.Select(tuple =>
        {
            var (invitation, organization, invitedBy) = tuple;
            invitation.SetOrganization(new OrganizationId(organization.Id));
            invitation.SetInvitedBy(new PersonId(invitedBy.Id));
            return (invitation, organization, invitedBy);
        }).ToList();
    }

}