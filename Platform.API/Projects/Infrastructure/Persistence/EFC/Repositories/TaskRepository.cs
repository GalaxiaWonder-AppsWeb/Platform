using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

public class TaskRepository(AppDbContext context) : BaseRepository<Task>(context), ITaskRepository
{
    public async Task<Task?> FindById(long id)
    {
        return await Context.Set<Task>()
            .Include(t => t.PersonId)
            .Include(t => t.Status)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Task?> FindAllTasksByMilestoneId(long milestoneId)
    {
        return await Context.Set<Task>()
            .Include(t => t.PersonId)
            .Include(t => t.Status)
            .Where(t => t.MilestoneId.Value == milestoneId)
            .FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Task>> FindAllTasksByPersonIdAndMilestoneId(long personId, long milestoneId)
    {
        return await Context.Set<Task>()
            .Include(t => t.PersonId)
            .Include(t => t.Status)
            .Where(t => t.PersonId.personId == personId && t.MilestoneId.Value == milestoneId)
            .ToListAsync();
    }
}