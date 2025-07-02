using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;

namespace Platform.API.Projects.Application.Internal.QueryServices;

public class ProjectTeamMemberQueryService(
    IProjectTeamMemberRepository projectTeamMemberRepository) : IProjectTeamMemberQueryService
{
    public async Task<IEnumerable<ProjectTeamMember>> Handle(GetAllProjectTeamMembersByProjectIdQuery query)
    {
        return await projectTeamMemberRepository.FindAllProjectTeamMembersByProjectId(query.ProjectId);
    }
}