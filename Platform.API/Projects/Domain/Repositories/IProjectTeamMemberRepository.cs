using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="ProjectTeamMember"/> entities.
/// </summary>
public interface IProjectTeamMemberRepository: IBaseRepository<ProjectTeamMember>
{
    /// <summary>
    /// Find a project team member by their unique identifier.
    /// </summary>
    /// <param name="id">
    /// Person id of the project team member to find.
    /// </param>
    /// <returns>
    /// Project team member if found, otherwise null.
    /// </returns>
    Task<ProjectTeamMember?> FindById(long id);
}