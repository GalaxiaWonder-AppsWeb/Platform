namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Represents the data required to update an existing organization.
/// </summary>
/// <param name="LegalName">The updated legal name of the organization.</param>
/// <param name="CommercialName">The updated commercial name of the organization.</param>
public record UpdateOrganizationResource(string LegalName, string CommercialName);