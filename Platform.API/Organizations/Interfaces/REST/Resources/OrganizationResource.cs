namespace Platform.API.Organizations.Interfaces.REST.Resources;

public record OrganizationResource(long Id, string LegalName, string? CommercialName, string Ruc, long CreatedBy, List<long>? Members);