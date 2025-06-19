using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Interfaces.REST.Resources;

namespace Platform.API.Organizations.Interfaces.REST.Assemblers;

/// <summary>
/// Assembler responsible for converting <see cref="CreateOrganizationResource"/> into a <see cref="CreateOrganizationCommand"/>.
/// </summary>
public class CreateOrganizationCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateOrganizationResource"/> into a <see cref="CreateOrganizationCommand"/>.
    /// </summary>
    /// <param name="resource">The input resource containing the organization creation data.</param>
    /// <returns>A command object encapsulating the creation details.</returns>
    public static CreateOrganizationCommand ToCommandFromResource(CreateOrganizationResource resource)
    {
        return new CreateOrganizationCommand(resource.LegalName, resource.CommercialName, resource.Ruc,
            resource.CreatedBy);
    }
}