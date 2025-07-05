using Platform.API.Change.Domain.Model.Commands;
using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Change.Interfaces.REST.Resources;

namespace Platform.API.Change.Interfaces.REST.Assemblers;

public class RespondToChangeProcessCommandFromResourceAssembler
{
    public static RespondToChangeProcessCommand ToCommandFromResource(
        long changeProcessId, RespondToChangeProcessResource resource)
    {
        return new RespondToChangeProcessCommand(
            changeProcessId,
            new ChangeResponse(resource.Response),
            new ChangeProcessStatus(Enum.Parse<ChangeProcessStatuses>(resource.Status))
        );
    }
}