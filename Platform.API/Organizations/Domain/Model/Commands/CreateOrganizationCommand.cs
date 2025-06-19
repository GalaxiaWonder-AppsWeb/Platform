namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to create a new organization in the system.
/// </summary>
/// <param name="LegalName">The legal name of the organization (required).</param>
/// <param name="CommercialName">The commercial name of the organization (optional).</param>
/// <param name="Ruc">The RUC (Registro Único de Contribuyentes) of the organization.</param>
/// <param name="CreatedBy">The ID of the person creating the organization.</param>
public record CreateOrganizationCommand(string LegalName, string? CommercialName, string Ruc, long CreatedBy);