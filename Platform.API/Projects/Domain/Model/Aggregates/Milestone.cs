using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Aggregates;

public partial class Milestone
{
    /// <summary>
    /// Represents the unique identifier for the project.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Name of the milestone
    /// A short, descriptive label that identifies the milestone within the project.
    /// </summary>
    public MilestoneName Name { get; set; }
    
    /// <summary>
    /// A detailed description of the milestone's purpose and objectives.
    /// </summary>
    public Description Description { get; set; }
    
    /// <summary>
    /// The identifier of the project to which this milestone belongs.
    /// Provides a contextual link between the milestone and its parent project.
    /// </summary>
    public ProjectId ProjectId { get; set; }

    /// <summary>
    /// Default constructor for the <see cref="Milestone"/>.
    /// </summary>
    public Milestone()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Milestone"/> class with the specified command.
    /// </summary>
    /// <param name="command">
    /// The command containing the necessary data to create a milestone.
    /// </param>
    public Milestone(CreateMilestoneCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        ProjectId = command.ProjectId;
    }
}