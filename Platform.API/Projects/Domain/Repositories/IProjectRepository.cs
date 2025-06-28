using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="Project"/> entities.
/// </summary>
public interface IProjectRepository :IBaseRepository<Project>
{
    Task<Project?> FindById(long id);
}