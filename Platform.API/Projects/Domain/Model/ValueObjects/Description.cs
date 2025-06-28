namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Represents a generic textual description used across different domain entities.
/// Ensures that the description is not null, empty, or exceeds a certain length.
/// </summary>
public record Description
{
    /// <summary>
    /// The textual description value.
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Constructs a new <see cref="Description"/> after validating the input.
    /// </summary>
    /// <param name="value">The textual content of the description</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="value"/> is null, empty, or whitespace.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="value"/> exceeds 200 characters.
    /// </exception>
    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Description cannot be empty", nameof(value));
        if (value.Length > 200)
            throw new ArgumentException("Description cannot exceed 200 characters", nameof(value));

        Value = value;
    }
}