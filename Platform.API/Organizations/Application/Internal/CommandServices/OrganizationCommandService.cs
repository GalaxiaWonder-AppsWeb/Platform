using Platform.API.IAM.Domain.Model.ValueObjects;
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


}