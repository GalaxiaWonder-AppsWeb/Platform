using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class CreateMilestoneCommandFromResourceAssembler
{
    public static CreateMilestoneCommand ToCommandFromResource(CreateMilestoneResource resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        return new CreateMilestoneCommand(
            new MilestoneName(resource.Name),
            new Description(resource.Description),
            new ProjectId(resource.ProjectId),
            new DateRange(resource.StartDate, resource.EndDate));
    }
}