using Platform.API.Billings.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Billings.Domain.Repositories;

public interface ITaskBudgetRepository : IBaseRepository<TaskBudget>
{
    Task<TaskBudget?> FindByTaskId(long taskId);
    Task<IEnumerable<TaskBudget>> FindTaskBudgetsByTaskIds(IEnumerable<long> taskIds);
}