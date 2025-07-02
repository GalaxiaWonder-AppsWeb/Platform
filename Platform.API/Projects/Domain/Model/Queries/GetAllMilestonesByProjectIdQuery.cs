namespace Platform.API.Projects.Domain.Model.Queries;

/// <summary>
/// Query object to retrieve all milestones associated with a project by its ID.
/// </summary>
/// <param name="ProjectId">
/// The unique identifier of the project whose milestones are to be retrieved.
/// </param>
public record GetAllMilestonesByProjectIdQuery(long ProjectId);