using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateProjectStatusCommandFromResourceAssembler
{
    public static UpdateProjectStatusCommand ToCommandFromResource(
        long id, UpdateProjectStatusResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        return new UpdateProjectStatusCommand(
            id,
            new ProjectStatus(Enum.Parse<ProjectStatuses>(resource.Status)));
    }
}