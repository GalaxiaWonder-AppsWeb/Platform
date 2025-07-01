using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;

namespace Platform.API.Projects.Application.Internal.QueryServices;

/// <summary>
/// Handles queries related to projects.
/// </summary>
/// <param name="projectRepository">
/// Represents the repository for accessing project data.
/// </param>
public class ProjectQueryService(
    IProjectRepository projectRepository
    ): IProjectQueryService
{
    public async Task<IEnumerable<Project>> Handle(GetAllProjectsByTeamMemberPersonIdQuery command)
    {
        return await projectRepository.FindAllProjectsByTeamMemberPersonIdAsync(command.PersonId);
    }
    public async Task<IEnumerable<Project>> Handle(GetAllProjectsByContractingEntityIdQuery query)
    {
        return await projectRepository.FindAllProjectsByContractingEntityId(query.Id);
    }
}