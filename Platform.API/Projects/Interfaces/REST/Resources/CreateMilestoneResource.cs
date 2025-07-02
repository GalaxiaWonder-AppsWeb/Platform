namespace Platform.API.Projects.Interfaces.REST.Resources;

/// <summary>
/// Resource that represents the data required to create a new milestone.
/// </summary>
/// <param name="Name">
/// Name of the milestone.
/// </param>
/// <param name="Description">
/// Description of the milestone with purpose and objectives.
/// </param>
/// <param name="ProjectId">
/// The unique identifier of the project to which this milestone belongs.
/// </param>
/// <param name="StartDate">
/// The start date of the milestone.
/// </param>
/// <param name="EndDate">
/// The end date of the milestone.
/// </param>
public record CreateMilestoneResource(string Name, string Description, long ProjectId, DateTimeOffset StartDate, DateTimeOffset EndDate);