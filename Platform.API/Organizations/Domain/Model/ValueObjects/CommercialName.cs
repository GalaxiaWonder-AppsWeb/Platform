namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Represents the commercial name of an organization.
/// </summary>
public record CommercialName
{
    /// <summary>
    /// Gets the value of the commercial name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommercialName"/> record.
    /// </summary>
    /// <param name="name">The commercial name. If null, it defaults to an empty string. Must not exceed 200 characters.</param>
    /// <exception cref="ArgumentException">Thrown when the name exceeds 200 characters.</exception>
    public CommercialName(string? name)
    {
        name ??= "";

        if (name.Length > 200)
            throw new ArgumentException("Name must be less than 200 characters", nameof(name));

        Name = name.Trim();
    }

    /// <summary>
    /// Returns the string representation of the commercial name.
    /// </summary>
    public override string ToString() => Name;
}
