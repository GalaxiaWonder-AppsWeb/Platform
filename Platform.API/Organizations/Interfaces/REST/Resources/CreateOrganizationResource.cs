namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource representing the input data required to create a new organization.
/// </summary>
/// <param name="LegalName">The legal name of the organization. This field is required.</param>
/// <param name="CommercialName">The commercial name of the organization. This field is optional.</param>
/// <param name="Ruc">The unique RUC (Registro Único de Contribuyentes) identifier of the organization.</param>
/// <param name="CreatedBy">The ID of the person who is creating the organization.</param>
public record CreateOrganizationResource(string LegalName, string? CommercialName, string Ruc, long CreatedBy);