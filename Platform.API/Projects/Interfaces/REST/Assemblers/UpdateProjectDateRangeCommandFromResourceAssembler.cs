using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class UpdateProjectDateRangeCommandFromResourceAssembler
{
    public static UpdateProjectDateRangeCommand ToCommandFromResource(
        long id, UpdateProjectDateRangeResource resource)
    {
        return new UpdateProjectDateRangeCommand(
            id,
            new DateRange(resource.StartDate,
                resource.EndDate));
    }
}