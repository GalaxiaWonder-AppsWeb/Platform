namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
///  Value object that represents the projectName of a milestone within a project schedule.
///  Ensures the projectName is not null, not blank, and does not exceed 100 characters.
/// </summary>
public record MilestoneName
{
    /// <summary>
    /// The textual projectName of the milestone
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Constructs a new <see cref="MilestoneName"/> instance after validating the input.
    /// </summary>
    /// <param name="value">
    /// the milestone projectName to validate
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided projectName is null, empty, or exceeds 100 characters.
    /// </exception>
    public MilestoneName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Milestone name cannot be null or empty.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Milestone name cannot exceed 20 characters.", nameof(value));
        }

        Value = value;
    }
}