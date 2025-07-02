using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class MilestoneResourceFromEntityAssembler
{
    public static async Task<MilestoneResource> ToResourceFromEntity(Milestone milestone)
    {
        return new MilestoneResource(
            milestone.Id,
            milestone.Name.Value,
            milestone.Description.Value,
            milestone.ProjectId.Value,
            milestone.DateRange.StartDate.Date,
            milestone.DateRange.EndDate.Date);
    }
}