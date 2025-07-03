using Microsoft.EntityFrameworkCore;
using Platform.API.Billings.Domain.Model.Aggregates;
using Platform.API.Billings.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Billings.Infrastructure.Persistence.EFC.Repositories;

public class TaskBudgetRepository(AppDbContext context) : BaseRepository<TaskBudget>(context) , ITaskBudgetRepository
{
    public async Task<IEnumerable<TaskBudget>> FindTaskBudgetsByTaskIds(IEnumerable<long> taskIds)
    {
        return await Context.Set<TaskBudget>()
            .Where(tb => taskIds.Contains(tb.TaskId.taskId))
            .ToListAsync();
    }

}