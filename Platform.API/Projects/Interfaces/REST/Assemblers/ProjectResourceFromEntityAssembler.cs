using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class ProjectResourceFromEntityAssembler
{
    public static ProjectResource ToResourceFromEntity(Project proj)
    {
        return new ProjectResource(
            proj.Id,
            proj.ProjectName.Value,
            proj.Description.Value,
            proj.DateRange.StartDate.Date,
            proj.DateRange.EndDate.Date,
            proj.OrganizationId.organizationId,
            proj.ContractingEntityId.personId);
    }
}