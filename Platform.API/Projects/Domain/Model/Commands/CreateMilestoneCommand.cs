using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// Command to create a new milestone within a project.
/// </summary>
/// <param name="Name">
/// Represents the name of the milestone.
/// </param>
/// <param name="Description">
/// Represents the description of the milestone with purpose and objectives.
/// </param>
/// <param name="ProjectId">
/// Represents the unique identifier of the project to which this milestone belongs.
/// </param>
/// <param name="DateRange">
/// Represents the date range during which the milestone is expected to be achieved.
/// </param>
public record CreateMilestoneCommand(MilestoneName Name, Description Description, ProjectId ProjectId, DateRange DateRange);