using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using TaskStatus = Platform.API.Projects.Domain.Model.Entities.TaskStatus;

namespace Platform.API.Projects.Domain.Model.Aggregates;

public class Task : MilestoneItem
{
    /// <summary>
    /// Represents the unique identifier for the project.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Specialty of the task.
    /// </summary>
    public Specialty Specialty { get; set; }
    
    public long SpecialtyId { get; set; }
    
    /// <summary>
    /// Enum representing the status of the task.
    /// </summary>
    public TaskStatus? Status { get; set; }
    
    public long StatusId { get; set; }
    
    /// <summary>
    /// The unique identifier of the person assigned to the task.
    /// </summary>
    public PersonId? PersonId { get; set; }
    
    /// <summary>
    /// Default constructor for the Task class.
    /// </summary>
    public Task () {}

    /// <summary>
    /// Initializes a new instance of the Task class with the specified command.
    /// </summary>
    /// <param name="command">
    /// Wrapper for the command that contains the necessary data to create a task.
    /// </param>
    public Task(CreateTaskCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        DateRange = command.DateRange;
        MilestoneId = command.MilestoneId;
        Specialty = command.Specialty;
        PersonId = command.PersonId;
    }

    /// <summary>
    /// Changes the status of the task to 'Draft'
    /// and clears the assigned person.
    /// </summary>
    /// <param name="status">
    /// Enum "DRAFT" representing the status of the task.
    /// </param>
    public void ToDraft(TaskStatus status)
    {
        Status = status;
        PersonId = null;
    }
    
    /// <summary>
    /// Change the assigned person for the task.
    /// </summary>
    /// <param name="personId">
    /// The unique identifier of the person to be assigned to the task.
    /// </param>
    public void ReassignPerson(PersonId personId)
    {
        PersonId = personId;
    }
    
    /// <summary>
    /// Change the status of the task.
    /// </summary>
    /// <param name="status">
    /// The unique identifier of the status to be assigned to the task.
    /// </param>
    public void ReassignStatus(TaskStatus status)
    {
        Status = status;
    }
    
    /// <summary>
    /// Change the name of the task.
    /// </summary>
    /// <param name="name">
    /// Name of the task to be assigned.
    /// </param>
    public void ReassignName(MilestoneItemName name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Change the description of the task.
    /// </summary>
    /// <param name="description">
    /// Description of the task to be assigned.
    /// </param>
    public void ReassignDescription(Description description)
    {
        Description = description;
    }
    
    /// <summary>
    /// Change the date range of the task.
    /// </summary>
    /// <param name="dateRange">
    /// Date Range of the task to be assigned, wrapping the start and end dates.
    /// </param>
    public void ReassignDateRange(DateRange dateRange)
    {
        DateRange = dateRange;
    }
    
    public void SetSpecialty(Specialty specialty)
    {
        Specialty = specialty;
    }
}