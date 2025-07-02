using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateMilestoneDateRangeCommandFromResourceAssembler
{
    public static UpdateMilestoneDateRangeCommand ToCommandFromResource(
        long id, UpdateMilestoneDateRangeResource resource)
    {
        return new UpdateMilestoneDateRangeCommand(
            id,
            new DateRange(resource.StartDate.Date,
                resource.EndDate.Date));
    }
}