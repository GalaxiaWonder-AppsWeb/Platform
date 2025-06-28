using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateProjectDescriptionCommandFromResourceAssembler
{
    public static UpdateProjectDescriptionCommand ToCommandFromResource(
        long id, UpdateProjectDescriptionResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        return new UpdateProjectDescriptionCommand(
            id,
            new Description(resource.Description));
    }
}