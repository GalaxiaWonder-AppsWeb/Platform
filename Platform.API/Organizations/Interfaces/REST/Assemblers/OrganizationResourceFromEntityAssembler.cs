using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

/// <summary>
/// Static assembler responsible for converting a domain <see cref="Organization"/> entity
/// into a client-facing <see cref="OrganizationResource"/> DTO.
/// </summary>
public static class OrganizationResourceFromEntityAssembler
{
    /// <summary>
    /// Maps an <see cref="Organization"/> domain entity to an <see cref="OrganizationResource"/> DTO.
    /// </summary>
    /// <param name="org">The organization domain entity to convert.</param>
    /// <returns>A DTO containing organization data for presentation or external consumption.</returns>
    public static OrganizationResource ToResourceFromEntity(Organization org)
    {
        return new OrganizationResource(
            org.Id,
            org.LegalName.Name,
            org.CommercialName?.Name ?? "",
            org.Ruc.Number,
            org.CreatedBy.personId,
            org.Status.Id,
            org.Status.Name.ToString(),
            org.OrganizationMemberIds.ToList(),
            org.OrganizationInvitationIds.ToList(),
            org.CreatedDate.Value
        );
    }
}
