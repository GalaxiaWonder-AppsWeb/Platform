namespace Platform.API.Billings.Interfaces.ACL;

public interface ITaskBudgetFacade
{
    Task CreateTaskBudget(long taskId, decimal amount, string currency);
}