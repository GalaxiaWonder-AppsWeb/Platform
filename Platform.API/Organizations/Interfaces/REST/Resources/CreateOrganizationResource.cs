namespace Platform.API.Organizations.Interfaces.REST.Resources;

public record CreateOrganizationResource(string LegalName, string? CommercialName, string Ruc, long CreatedBy);