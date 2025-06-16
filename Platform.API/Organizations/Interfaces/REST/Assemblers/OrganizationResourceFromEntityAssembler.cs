using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

public static class OrganizationResourceFromEntityAssembler
{
    public static OrganizationResource ToResourceFromEntity(Organization org)
    {
        return new OrganizationResource(
            org.Id,
            org.LegalName.Name,
            org.CommercialName.Name,
            org.Ruc.Number,
            org.CreatedBy.personId,
            org.OrganizationStatusId,
            org.Status.Name.ToString(),
            org.OrganizationMemberIds,
            org.OrganizationInvitationIds
        );
    }
}
