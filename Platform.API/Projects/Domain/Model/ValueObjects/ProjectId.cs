namespace Platform.API.Projects.Domain.Model.ValueObjects;

public record ProjectId
{
    /// <summary>
    /// Represents the unique identifier for the project.
    /// </summary>
    public long Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectId"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the project.</param>
    public ProjectId(long id)
    {
        Value = id;
    }

    /// <summary>
    /// Default constructor for the <see cref="ProjectId"/>.
    /// </summary>
    public ProjectId() : this(0L) { }
}