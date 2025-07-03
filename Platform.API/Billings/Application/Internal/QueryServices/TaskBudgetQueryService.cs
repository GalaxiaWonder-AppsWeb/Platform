using Platform.API.Billings.Domain.Model.Queries;
using Platform.API.Billings.Domain.Repositories;
using Platform.API.Billings.Domain.Services;
using Platform.API.Projects.Interfaces.ACL;

namespace Platform.API.Billings.Application.Internal.QueryServices;

public class TaskBudgetQueryService(
    ITaskBudgetRepository taskBudgetRepository,
    IProjectFacade projectFacade) : ITaskBudgetQueryService
{
    public async Task<decimal> Handle(GetTotalTasksBudgetByProjectIdQuery query)
    {
        var taskIds = await projectFacade.GetTaskIdsByProjectId(query.ProjectId);
        if (!taskIds.Any())
        {
            return 0m;
        }

        var budgets = await taskBudgetRepository.FindTaskBudgetsByTaskIds(taskIds);

        return budgets.Sum(b => b.Money.Amount);
    }
}