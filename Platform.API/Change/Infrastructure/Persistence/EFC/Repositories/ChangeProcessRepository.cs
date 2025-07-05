using Microsoft.EntityFrameworkCore;
using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Change.Infrastructure.Persistence.EFC.Repositories;

public class ChangeProcessRepository(AppDbContext context) : BaseRepository<ChangeProcess>(context), IChangeProcessRepository
{
    /// <summary>
    /// Find an existing <see cref="ChangeProcess"/> by its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the change process.
    /// </param>
    /// <returns>
    /// The matching <see cref="ChangeProcess"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ChangeProcess?> FindById(long id)
    {
        return await Context.Set<ChangeProcess>()
            .Include(cp => cp.Origin)
            .Include(cp => cp.Status)
            .Include(cp => cp.ProjectId)
            .FirstOrDefaultAsync(cp => cp.Id == id);
    }
    
    /// <summary>
    /// Find an existing <see cref="ChangeProcess"/> by his linked project.
    /// </summary>
    /// <param name="projectId">
    /// The unique identifier of the project associated with the change process.
    /// </param>
    /// <returns>
    /// The matching <see cref="ChangeProcess"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ChangeProcess?> FindByProjectId(long projectId)
    {
        return await Context.Set<ChangeProcess>()
            .Where(cp => cp.ProjectId.Value == projectId)
            .OrderByDescending(cp => cp.CreatedDate)
            .Include(cp => cp.Origin)
            .Include(cp => cp.Status)
            .FirstOrDefaultAsync();
    }

}