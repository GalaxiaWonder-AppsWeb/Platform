using Platform.API.Billings.Domain.Model.Commands;
using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Billings.Domain.Model.Aggregates;

/// <summary>
/// Represents a budget allocated for a specific task within a project.
/// </summary>
public class TaskBudget
{
    /// <summary>
    /// Represents the unique identifier for the project.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// The unique identifier of the task associated with this budget.
    /// </summary>
    public TaskId TaskId { get; set; }
    
    /// <summary>
    /// The amount of money allocated for the task budget
    /// with the currency specified.
    /// </summary>
    public Money Money { get; set; }
    
    /// <summary>
    /// Defailt constructor for the <see cref="TaskBudget"/>.
    /// </summary>
    public TaskBudget(){}
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TaskBudget"/> class with the specified command.
    /// </summary>
    /// <param name="command">
    /// The command containing the necessary data to create a task budget.
    /// Wraps a <see cref="TaskId"/> and a <see cref="Money"/> object.
    /// </param>
    public TaskBudget(CreateTaskBudgetCommand command)
    {
        TaskId = command.TaskId;
        Money = command.Money;
    }
}