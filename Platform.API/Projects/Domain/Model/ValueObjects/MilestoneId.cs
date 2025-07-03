namespace Platform.API.Projects.Domain.Model.ValueObjects;

public record MilestoneId
{
    /// <summary>
    /// Represents the unique identifier for the milestone.
    /// </summary>
    public long Value { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MilestoneId"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier for the milestone.
    /// </param>
    public MilestoneId(long id)
    {
        Value = id;
    }

    /// <summary>
    /// Default constructor for the <see cref="MilestoneId"/>.
    /// </summary>
    public MilestoneId() : this(0L) { }
}