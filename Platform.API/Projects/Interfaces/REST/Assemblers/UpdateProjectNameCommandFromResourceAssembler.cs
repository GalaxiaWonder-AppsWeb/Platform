using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateProjectNameCommandFromResourceAssembler
{
    public static UpdateProjectNameCommand ToCommandFromResource(
        long id, UpdateProjectNameResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        return new UpdateProjectNameCommand(
            id,
            new ProjectName(resource.Name));
    }
}