using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="ProjectStatus"/> entities.
/// </summary>
/// <param name="context"></param>
public class ProjectStatusRepository(AppDbContext context) : BaseRepository<ProjectStatus>(context), IProjectStatusRepository
{
    /// <summary>
    /// Find an <see cref="ProjectStatus"/> by its name.
    /// </summary>
    /// <param name="name"> The name of the status as a ProjectStatuses</param>
    /// <returns>
    /// The matching <see cref="ProjectStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ProjectStatus?> FindByName(string name)
    {
        return await Context.Set<ProjectStatus>()
            .FirstOrDefaultAsync(ps => ps.Name.ToString() == name);
    }
}