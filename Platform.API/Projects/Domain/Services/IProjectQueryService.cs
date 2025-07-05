using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;

namespace Platform.API.Projects.Domain.Services;

public interface IProjectQueryService
{
    Task<Project?> Handle(GetProjectByIdQuery query);
    Task<IEnumerable<Project>> Handle(GetAllProjectsByTeamMemberPersonIdQuery command);
    Task<IEnumerable<Project>> Handle(GetAllProjectsByContractingEntityIdQuery query);
}