using Platform.API.Change.Domain.Model.Commands;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Change.Interfaces.REST.Resources;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Change.Interfaces.REST.Assemblers;

public class CreateChangeProcessComandFromResourceAssembler
{
    public static CreateChangeProcessCommand ToCommandFromResource(
        long projectId, CreateChangeProcessResource resource)
    {
        return new CreateChangeProcessCommand(
            new Justification(resource.Justification),
            new ProjectId(projectId));
    }
}