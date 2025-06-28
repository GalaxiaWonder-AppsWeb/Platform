using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="ProjectStatus"/> entities.
/// </summary>
public interface IProjectStatusRepository: IBaseRepository<ProjectStatus>
{
    /// <summary>
    /// Finds a <see cref="ProjectStatus"/> by its name.
    /// </summary>
    /// <param name="name">
    /// The name of the project status (e.g., BASIC_STUDIES, CHANGE_PENDING, APPROVED).
    /// </param>
    /// <returns>
    /// The <see cref="ProjectStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ProjectStatus?> FindByName(ProjectStatuses name);
}