namespace Platform.API.Change.Domain.Model.ValueObjects;

/// <summary>
/// Represents a change process justification, change order description and change response notes.
/// Ensures that the justification is not null, not blank and does not exceed 100 characters.
/// Internally wraps a string value.
/// </summary>
public record Justification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Justification"/> class.
    /// </summary>
    /// <param name="value">
    /// The content of the justification.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throw if the value is null, empty or exceeds 100 characters.
    /// </exception>
    public Justification(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Justification cannot be null or empty.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Justification cannot exceed 100 characters.", nameof(value));
        }

        Value = value;
    }

    public string Value { get; init; }
}