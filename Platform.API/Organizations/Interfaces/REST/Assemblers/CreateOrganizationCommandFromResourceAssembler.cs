using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public class CreateOrganizationCommandFromResourceAssembler
{
    public static CreateOrganizationCommand ToCommandFromResource(CreateOrganizationResource resource)
    {
        return new CreateOrganizationCommand(resource.LegalName, resource.CommercialName, resource.Ruc,
            resource.CreatedBy);
    }
}