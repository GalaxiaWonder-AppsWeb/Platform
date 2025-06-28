namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Represents the projectName of a project in the system
/// Ensures that the projectName is non-null, non-blank, and within a valid lenght range. 
/// </summary>
public record ProjectName
{
    /// <summary>
    /// The string representation of the project projectName
    /// </summary>
    public string Value { get; }
    
    /// <summary>
    /// Constructs a new instance of <see cref="ProjectName"/>.
    /// </summary>
    /// <param name="value">
    /// The project name value.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the project name is null, empty, or exceeds the maximum length of 100 characters.
    /// </exception>
    public ProjectName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Project name cannot be null or empty.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Project name cannot exceed 100 characters.", nameof(value));
        }

        Value = value;
    }
}