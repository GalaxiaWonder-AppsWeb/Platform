namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Represents the projectName of a milestone item (such as a task or a meeting).
/// Ensures the projectName is not null, not blank, and does not exceed 100 characters.
/// </summary>
public record MilestoneItemName
{
    /// <summary>
    /// The textual projectName of the milestone item
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Constructs a new instance of <see cref="MilestoneItemName"/>.
    /// </summary>
    /// <param name="value">
    /// The projectName of the milestone item.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the projectName is null, empty, or exceeds 100 characters.
    /// </exception>
    public MilestoneItemName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Milestone item name cannot be null or empty.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Milestone item name cannot exceed 100 characters.", nameof(value));
        }

        Value = value;
    }
}