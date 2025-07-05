using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Model.ValueObjects;
using TaskStatus = Platform.API.Projects.Domain.Model.Entities.TaskStatus;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class CreateTaskCommandFromResourceAssembler
{
    public static CreateTaskCommand ToCommandFromResource(CreateTaskResource resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        return new CreateTaskCommand(
            new MilestoneItemName(resource.Name),
            new Description(resource.Description),
            new DateRange(resource.StartDate.Date, resource.EndDate.Date),
            new MilestoneId(resource.MilestoneId),
            new Specialty(Enum.Parse<Specialties>(resource.Specialty)),
            resource.Status is not null ? new TaskStatus(Enum.Parse<TaskStatuses>(resource.Status)) : null,
            resource.PersonId is not null ? new PersonId(resource.PersonId.Value) : null,
            new Money(resource.Amount,
                "USD")
            );
    }
}