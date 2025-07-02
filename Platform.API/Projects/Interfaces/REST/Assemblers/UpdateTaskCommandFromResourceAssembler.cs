using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;
using TaskStatus = Platform.API.Projects.Domain.Model.Entities.TaskStatus;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateTaskCommandFromResourceAssembler
{
    public static UpdateTaskCommand ToCommandFromResource(long id, UpdateTaskResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        MilestoneItemName? name = resource.Name != null
            ? new MilestoneItemName(resource.Name)
            : null;

        Description? description = resource.Description != null
            ? new Description(resource.Description)
            : null;

        DateRange? dateRange = (resource.StartDate.HasValue && resource.EndDate.HasValue)
            ? new DateRange(resource.StartDate.Value.Date, resource.EndDate.Value.Date)
            : null;

        TaskStatus? status = !string.IsNullOrWhiteSpace(resource.Status)
            ? new TaskStatus(Enum.Parse<TaskStatuses>(resource.Status))
            : null;

        PersonId? personId = resource.PersonId.HasValue
            ? new PersonId(resource.PersonId.Value)
            : null;

        return new UpdateTaskCommand(
            id,
            name,
            description,
            dateRange,
            status,
            personId,
            resource.RemovePerson
        );
    }

}