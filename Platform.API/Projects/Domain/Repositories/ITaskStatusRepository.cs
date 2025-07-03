using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;
using TaskStatus = Model.Entities.TaskStatus;

public interface ITaskStatusRepository : IBaseRepository<TaskStatus>
{
    /// <summary>
    /// Find a <see cref="TaskStatus"/> by its name.
    /// </summary>
    /// <param name="name">
    /// The name of the task status (e.g., DRAFT, APPROVED, REJECTED).
    /// </param>
    /// <returns>
    /// The <see cref="TaskStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TaskStatus?> FindByName(string name);
}