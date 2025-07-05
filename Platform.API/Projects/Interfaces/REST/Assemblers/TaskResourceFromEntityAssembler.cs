using Platform.API.Projects.Interfaces.REST.Resources;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class TaskResourceFromEntityAssembler
{
    public static async Task<TaskResource> ToResourceFromEntity(Task task)
    {
        if (task is null)
        {
            return null;
        }

        return new TaskResource(
            task.Id,
            task.Name.Value,
            task.Description.Value,
            task.DateRange.StartDate.Date,
            task.DateRange.EndDate.Date,
            task.MilestoneId.Value,
            task.Specialty.Name.ToString(),
            task.Status?.Name.ToString(),    
            task.PersonId?.personId);
    }
}