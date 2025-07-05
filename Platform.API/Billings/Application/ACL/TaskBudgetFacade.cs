using Platform.API.Billings.Domain.Model.Commands;
using Platform.API.Billings.Domain.Services;
using Platform.API.Billings.Interfaces.ACL;
using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Billings.Application.ACL;

public class TaskBudgetFacade(
    ITaskBudgetCommandService taskBudgetCommandService) : ITaskBudgetFacade
{
    public async Task CreateTaskBudget(long taskId, decimal amount, string currency)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
        }

        var command = new CreateTaskBudgetCommand(new TaskId(taskId), new Money(amount, currency));
        await taskBudgetCommandService.Handle(command);
    }
}