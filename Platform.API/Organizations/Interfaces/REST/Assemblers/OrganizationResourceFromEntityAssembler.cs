using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public class OrganizationResourceFromEntityAssembler
{
    public static OrganizationResource ToResourceFromEntity(Organization organization)
    {
        return new OrganizationResource(organization.Id, organization.LegalName.Name, organization.CommercialName.Name,
            organization.Ruc.Number, organization.CreatedBy.personId, organization.OrganizationMemberIds.ToList());
    }
}