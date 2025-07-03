using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Interfaces.REST.Resources;

namespace Platform.API.Change.Interfaces.REST.Assemblers;

public class ChangeProcessResourceFromEntityAssembler
{
    public static async Task<ChangeProcessResource> ToResourceFromEntity(
        ChangeProcess changeProcess)
    {
        if (changeProcess is null)
        {
            return null;
        }

        return new ChangeProcessResource(
            changeProcess.Id,
            changeProcess.Origin.Name.ToString(),
            changeProcess.Status.Name.ToString(),
            changeProcess.Justification.Value,
            changeProcess.Response?.Value,
            changeProcess.ProjectId.Value
        );
    }
}