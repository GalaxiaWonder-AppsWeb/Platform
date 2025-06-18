namespace Platform.API.Organizations.Domain.Model.Commands;

public record UpdateOrganizationCommand
{
    public long Id { get; }
    public string LegalName { get; }
    public string CommercialName { get; }

    public UpdateOrganizationCommand(long id, string legalName, string commercialName)
    {
        if (Id == 0L)
            throw new ArgumentNullException("id cannot be zero");
        if(string.IsNullOrEmpty(legalName))
            throw new ArgumentNullException("legal name cannot be null or empty");
        if (string.IsNullOrEmpty(commercialName))
            throw new ArgumentNullException("commercial name cannot be null or empty");
        
        this.Id = id;
        this.LegalName = legalName;
        this.CommercialName = commercialName;
    }
}