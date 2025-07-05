using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;

namespace Platform.API.Projects.Application.Internal.QueryServices;

public class MilestoneQueryService(
    IMilestoneRepository milestoneRepository
    ): IMilestoneQueryService
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
    public async Task<IEnumerable<Milestone>> Handle(GetAllMilestonesByProjectIdQuery query)
    {
        return await milestoneRepository.FindAllMilestonesByProjectId(query.ProjectId);
    }
}