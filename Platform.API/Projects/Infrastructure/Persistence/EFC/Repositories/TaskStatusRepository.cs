using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using TaskStatus = Platform.API.Projects.Domain.Model.Entities.TaskStatus;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="TaskStatus"/> entities.
/// </summary>
/// <param name="context"></param>
public class TaskStatusRepository(AppDbContext context) : BaseRepository<TaskStatus>(context), ITaskStatusRepository
{
    /// <summary>
    /// Find a <see cref="TaskStatus"/> by its name.
    /// </summary>
    /// <param name="name">
    /// The name of the status as a <see cref="TaskStatuses"/>.
    /// </param>
    /// <returns>
    /// The matching <see cref="TaskStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<TaskStatus?> FindByName(string name)
    {
        return await Context.Set<TaskStatus>()
            .FirstOrDefaultAsync(ts => ts.Name.ToString() == name);
    }
}