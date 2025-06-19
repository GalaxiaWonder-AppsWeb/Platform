using Platform.API.IAM.Application.ACL;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;
using Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.Shared.Domain.Repositories;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Application.Internal.CommandServices;

public class OrganizationCommandService(
    IOrganizationRepository organizationRepository,
    IOrganizationMemberRepository organizationMemberRepository,
    IOrganizationMemberTypeRepository organizationMemberTypeRepository,
    IIAMContextFacade iamContext,
    IOrganizationInvitationStatusRepository organizationInvitationStatusRepository,
    IOrganizationInvitationRepository organizationInvitationRepository,
    IUnitOfWork unitOfWork,
    IOrganizationStatusRepository organizationStatusRepository):IOrganizationCommandService
{
    public async Task<Organization> Handle(CreateOrganizationCommand command)
    {
        if (organizationRepository.ExistsByRuc(command.Ruc))
            throw new Exception("Organization with same RUC already exists");
        
        var organization = new Organization(
            new LegalName(command.LegalName),
            new CommercialName(command.CommercialName),
            new RUC(command.Ruc),
            new PersonId(command.CreatedBy)
        );
        
        var activeStatus = await organizationStatusRepository.FindByNameAsync("ACTIVE");
        organization.AssignStatus(activeStatus!);
        
        await organizationRepository.AddAsync(organization);
        await unitOfWork.CompleteAsync(); // Genera el ID
        
        var contractorMemberType = await organizationMemberTypeRepository.FindByNameAsync("CONTRACTOR");
        
        var member = new OrganizationMember(
            new OrganizationId(organization.Id), 
            new PersonId(command.CreatedBy),
            contractorMemberType
        );

        organization.AddOrganizationMember(command.CreatedBy);
        await organizationMemberRepository.AddAsync(member);
        await unitOfWork.CompleteAsync();

        return organization;
    }

    public async Task Handle(DeleteOrganizationCommand command)
    {
        var organization = await organizationRepository.FindByIdAsync(command.Id);
        if (organization == null) 
            throw new ArgumentException("Organization doesn't exist");
    
        organizationRepository.Remove(organization);
        await unitOfWork.CompleteAsync();
    }

    public async Task<Organization> Handle(UpdateOrganizationCommand command)
    {
        var organization = await organizationRepository.FindByIdAsync(command.Id);
        if (organization == null)
            throw new ArgumentException("Organization doesn't exist");

        if (organization.LegalName.Name != command.LegalName)
            organization.EditLegalName(new LegalName(command.LegalName));

        if (organization.CommercialName.Name != command.CommercialName)
            organization.EditCommercialName(new CommercialName(command.CommercialName));

        try
        {
            organizationRepository.Update(organization);
            await unitOfWork.CompleteAsync();
            return organization;
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Error while updating organization: {e.Message}");
        }
    }

    public async Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(InvitePersonToOrganizationByEmailCommand command)
    {
        var person = await iamContext.GetProfileDetailsByEmailAsync(command.Email)
                     ?? throw new Exception("Unable to get profile details");

        var personId = new PersonId(person.Id);

        var organization = await organizationRepository.FindByIdAsync(command.OrganizationId)
                           ?? throw new Exception("Organization doesn't exist");

        var pendingStatus = await organizationInvitationStatusRepository.FindByNameAsync("PENDING")
                            ?? throw new Exception("Invitation status 'PENDING' not found");

        var invitation = new OrganizationInvitation(
            new OrganizationId(organization.Id),
            personId,
            organization.CreatedBy,
            pendingStatus
        );

        await organizationInvitationRepository.AddAsync(invitation);
        await unitOfWork.CompleteAsync();

        var persistedInvitation = await organizationInvitationRepository.FindLatestInvitation(
            organization.Id, personId.personId
        ) ?? throw new Exception("Failed to persist invitation");

        organization.AddOrganizationInvitation(persistedInvitation.Id);
        organizationRepository.Update(organization);
        await unitOfWork.CompleteAsync();

        var profileDetails = await iamContext.GetProfileDetailsByIdAsync(personId.personId)
                             ?? throw new Exception("Failed to get profile details");

        return (organization, persistedInvitation, profileDetails);
    }

    public async Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(AcceptInvitationCommand command)
    {
        var invitation = await organizationInvitationRepository.FindByIdAsync(command.Id)
                         ?? throw new Exception("Invitation doesn't exist");
        
        var organization = await organizationRepository.FindByIdAsync(invitation.OrganizationId.organizationId)
                           ?? throw new Exception("Organization not found");

        var acceptedStatus = await organizationInvitationStatusRepository.FindByNameAsync("ACCEPTED")
                             ?? throw new Exception("Invitation status 'ACCEPTED' not found");
        

        var memberType = await organizationMemberTypeRepository.FindByNameAsync("WORKER")
                         ?? throw new Exception("'WORKER' role not found");

        invitation.ChangeStatus(acceptedStatus);
        organizationInvitationRepository.Update(invitation);

        var organizationMember = new OrganizationMember(
            new OrganizationId(organization.Id), 
            new PersonId(invitation.PersonId.personId), 
            memberType
        );
        await organizationMemberRepository.AddAsync(organizationMember);

        await unitOfWork.CompleteAsync();

        var profileDetails = await iamContext.GetProfileDetailsByIdAsync(invitation.PersonId.personId)
                             ?? throw new Exception("Failed to retrieve profile details");

        return (organization, invitation, profileDetails);
    }
    
    public async Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(RejectInvitationCommand command)
    {
        var invitation = await organizationInvitationRepository.FindByIdAsync(command.Id)
                         ?? throw new Exception("Invitation doesn't exist");
        if (invitation.OrganizationId == null)
            throw new Exception("Invitation.OrganizationId is null");

        if (invitation.Status == null)
            throw new Exception("Invitation.Status is null");

        if (invitation.Status.Name == null)
            throw new Exception("Invitation.Status.Name is null");

        if (invitation.PersonId == null)
            throw new Exception("Invitation.PersonId is null");

        var organization = await organizationRepository.FindByIdAsync(invitation.OrganizationId.organizationId)
                           ?? throw new Exception("Organization not found");
        var rejectedStatus = await organizationInvitationStatusRepository.FindByNameAsync(nameof(OrganizationInvitationStatuses.REJECTED))
                             ?? throw new Exception($"Invitation status '{nameof(OrganizationInvitationStatuses.REJECTED)}' not found");


        if (invitation.Status.Name.ToString() != "PENDING")
            throw new InvalidOperationException("Only pending invitations can be rejected");

        invitation.ChangeStatus(rejectedStatus);
        organizationInvitationRepository.Update(invitation);
        await unitOfWork.CompleteAsync();

        var profileDetails = await iamContext.GetProfileDetailsByIdAsync(invitation.PersonId.personId)
                             ?? throw new Exception("Failed to retrieve profile details");

        return (organization, invitation, profileDetails);
    }
    
    public async Task Handle(DeleteOrganizationMemberCommand command)
    {
        var member = await organizationMemberRepository.FindByIdAsync(command.OrganizationMemberId)
                     ?? throw new Exception($"Organization member with ID {command.OrganizationMemberId} not found");

        if (member.MemberType.Name.ToString().Equals("CONTRACTOR")) 
            throw new Exception("Only contract members can be deleted");
        
        organizationMemberRepository.Remove(member);

        await unitOfWork.CompleteAsync();
    }


}