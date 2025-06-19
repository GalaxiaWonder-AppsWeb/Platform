namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
/// Command to update the basic information of an existing organization.
/// </summary>
public record UpdateOrganizationCommand
{
    /// <summary>
    /// Gets the unique identifier of the organization.
    /// </summary>
    public long Id { get; }
    
    /// <summary>
    /// Gets the legal name of the organization.
    /// </summary>
    public string LegalName { get; }
    
    /// <summary>
    /// Gets the commercial name of the organization.
    /// </summary>
    public string CommercialName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateOrganizationCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the organization.</param>
    /// <param name="legalName">The new legal name of the organization.</param>
    /// <param name="commercialName">The new commercial name of the organization.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is less than or equal to zero, or <paramref name="legalName"/> is null or whitespace.</exception>
    public UpdateOrganizationCommand(long id, string legalName, string commercialName)
    {
        if (id <= 0) throw new ArgumentException("Organization ID must be a positive number", nameof(id));
        if (string.IsNullOrWhiteSpace(legalName)) throw new ArgumentException("Legal name cannot be empty", nameof(legalName));
    
        Id = id;
        LegalName = legalName;
        CommercialName = commercialName;
    }
}