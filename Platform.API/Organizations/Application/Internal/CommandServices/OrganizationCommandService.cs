using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;
using Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Application.Internal.CommandServices;

public class OrganizationCommandService(
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork,
    IOrganizationStatusRepository organizationStatusRepository):IOrganizationCommandService
{
    public async Task<Organization> Handle(CreateOrganizationCommand command)
    {
        if(organizationRepository.ExistsByRuc(command.Ruc))
            throw new Exception("Organization with same RUC already exists");

        var status = await organizationStatusRepository.FindByNameAsync("ACTIVE");

        if (status == null)
            throw new Exception("Default organization status not found");

        var organization = new Organization(
            new LegalName(command.LegalName),
            new CommercialName(command.CommercialName),
            new RUC(command.Ruc),
            new PersonId(command.CreatedBy),
            status
        );
        
        organization.AddOrganizationMember(command.CreatedBy);
        
        await organizationRepository.AddAsync(organization);
        await unitOfWork.CompleteAsync();
        
        return organization;
    }
}