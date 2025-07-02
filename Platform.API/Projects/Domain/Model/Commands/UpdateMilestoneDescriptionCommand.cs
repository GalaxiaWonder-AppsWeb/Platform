using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// Command to update the description of an existing milestone.
/// </summary>
/// <param name="Id">
/// Represents the unique identifier of the milestone to be updated.
/// </param>
/// <param name="MilestoneDescription">
/// Represents the new description of the milestone, detailing its purpose and objectives.
/// </param>
public record UpdateMilestoneDescriptionCommand(long Id, Description MilestoneDescription);