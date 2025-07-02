using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateMilestoneDescriptionCommandFromResourceAssembler
{
    public static UpdateMilestoneDescriptionCommand ToCommandFromResource(
        long id, UpdateMilestoneDescriptionResource resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null.");
        }

        return new UpdateMilestoneDescriptionCommand(
            id,
            new Description(resource.Description));
    }
}