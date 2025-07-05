using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;

namespace Platform.API.Projects.Domain.Services;
/// <summary>
/// Query service interface for handling operations related to milestones.
/// </summary>
public interface IMilestoneQueryService
{
    /// <summary>
    /// Gives all milestones associated with a specific project ID.
    /// </summary>
    /// <param name="query">
    /// Represents the query containing the project ID for which milestones are to be retrieved.
    /// </param>
    /// <returns>
    /// All the milestones associated with the specified project ID.
    /// </returns>
    Task<IEnumerable<Milestone>> Handle(GetAllMilestonesByProjectIdQuery query);
}