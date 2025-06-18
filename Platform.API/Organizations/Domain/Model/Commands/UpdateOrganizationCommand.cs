namespace Platform.API.Organizations.Domain.Model.Commands;

public record UpdateOrganizationCommand
{
    public long Id { get; }
    public string LegalName { get; }
    public string CommercialName { get; }

    public UpdateOrganizationCommand(long id, string legalName, string commercialName)
    {
        if (id <= 0) throw new ArgumentException("Organization ID must be a positive number", nameof(id));
        if (string.IsNullOrWhiteSpace(legalName)) throw new ArgumentException("Legal name cannot be empty", nameof(legalName));
    
        Id = id;
        LegalName = legalName;
        CommercialName = commercialName;
    }
}