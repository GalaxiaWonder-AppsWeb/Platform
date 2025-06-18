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
        
        // Obtener status activo desde repositorio
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

}