using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Domain.Services;

namespace Platform.API.Organizations.Application.Internal.CommandServices;

public class OrganizationCommandService(
    IOrganizationRepository organizationRepository):IOrganizationCommandService
{
    public async Task<Organization> Handle(CreateOrganizationCommand command)
    {
        if(organizationRepository.ExistsByRuc(command.Ruc))
            throw new Exception("Organization with same RUC already exists");

        var organization = new Organization(new LegalName(command.LegalName), new CommercialName(command.CommercialName), new RUC(command.Ruc), new PersonId(command.CreatedBy));
        organization.AddOrganizationMember(command.CreatedBy);
        
        await organizationRepository.AddAsync(organization);
        
        return organization;
    }
}