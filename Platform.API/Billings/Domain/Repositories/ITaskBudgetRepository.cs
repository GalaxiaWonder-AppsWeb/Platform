using Platform.API.Billings.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Billings.Domain.Repositories;

public interface ITaskBudgetRepository : IBaseRepository<TaskBudget>
{
    Task<decimal> FindTotalTasksBudgetByProjectId(long projectId);
}