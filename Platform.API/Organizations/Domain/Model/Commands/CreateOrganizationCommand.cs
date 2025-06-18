namespace Platform.API.Organizations.Domain.Model.Commands;

public record CreateOrganizationCommand(string LegalName, string? CommercialName, string Ruc, long CreatedBy);