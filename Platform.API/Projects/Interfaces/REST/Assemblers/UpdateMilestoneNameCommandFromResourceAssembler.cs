using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateMilestoneNameCommandFromResourceAssembler
{
    public static UpdateMilestoneNameCommand ToCommandFromResource(
        long id, UpdateMilestoneNameResource resource)
    {
        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource), "Resource cannot be null");
        }

        return new UpdateMilestoneNameCommand(
            id,
            new MilestoneName(resource.Name));
    }
}