namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Represents the legal name of an organization.
/// </summary>
public record LegalName
{
    /// <summary>
    /// Gets the value of the legal name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LegalName"/> record.
    /// </summary>
    /// <param name="name">The legal name of the organization. Must be non-empty and have a maximum length of 500 characters.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="name"/> is null, empty, whitespace, or exceeds 500 characters.
    /// </exception>
    public LegalName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        if (name.Length > 500)
            throw new ArgumentException("Name must be less than 500 characters", nameof(name));
        Name = name.Trim();
    }
    
    /// <summary>
    /// Returns the string representation of the legal name.
    /// </summary>
    public override string ToString() => Name;
}