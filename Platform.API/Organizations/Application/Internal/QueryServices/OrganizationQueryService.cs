using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Application.Internal.QueryServices;

public class OrganizationQueryService(
    IOrganizationRepository organizationRepository,
    IOrganizationInvitationRepository organizationInvitationRepository,
    IIAMContextFacade iamContext,
    IOrganizationMemberRepository organizationMemberRepository
    ) : IOrganizationQueryService
{
    public async Task<Organization?> Handle(GetOrganizationByIdQuery query)
    {
        return await organizationRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery query)
    {
        return await organizationRepository.ListAsync();
    }
    
    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsByMemberPersonIdQuery query)
    {
        return await organizationRepository.FindOrganizationsByOrganizationMemberPersonId(query.Id);
    }

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

}