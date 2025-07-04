using Platform.API.Billings.Domain.Model.Aggregates;
using Platform.API.Billings.Domain.Model.Commands;

namespace Platform.API.Billings.Domain.Services;

public interface ITaskBudgetCommandService
{
    Task<TaskBudget?> Handle(CreateTaskBudgetCommand command);
}