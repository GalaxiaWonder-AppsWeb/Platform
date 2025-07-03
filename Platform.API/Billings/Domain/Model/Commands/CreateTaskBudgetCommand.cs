using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Billings.Domain.Model.Commands;

public record CreateTaskBudgetCommand(TaskId TaskId, Money Money);