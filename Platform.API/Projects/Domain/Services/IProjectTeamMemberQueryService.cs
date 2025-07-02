using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;

namespace Platform.API.Projects.Domain.Services;

public interface IProjectTeamMemberQueryService
{
    Task<IEnumerable<ProjectTeamMember>> Handle(GetAllProjectTeamMembersByProjectIdQuery query);
}