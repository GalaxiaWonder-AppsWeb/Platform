using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Entities;

public class TaskStatus
{
    /// <summary>
    /// Database identifier for the project status entity.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Enum description representing the status of the tasks.
    /// </summary>
    public TaskStatuses Name { get; set; }
    
    /// <summary>
    /// Default constructor for the <see cref="TaskStatus"/> class.
    /// </summary>
    public TaskStatus()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TaskStatus"/> class with the specified status name.
    /// </summary>
    /// <param name="name">
    /// The status name of the task, represented by the <see cref="TaskStatuses"/> enum.
    /// </param>
    public TaskStatus(TaskStatuses name)
    {
        Name = name;
    }
    
    public TaskStatuses GetName()
    {
        return Name;
    }
}