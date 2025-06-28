using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="ProjectTeamMember"/> entities.
/// </summary>
public interface IProjectTeamMemberRepository: IBaseRepository<ProjectTeamMember>
{
    
}