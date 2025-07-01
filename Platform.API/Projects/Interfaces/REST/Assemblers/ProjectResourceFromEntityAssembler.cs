using Platform.API.Organizations.Application.ACL;
using Platform.API.Organizations.Interfaces.ACL;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class ProjectResourceFromEntityAssembler
{
    private readonly IOrganizationFacade _orgFacade;

    public ProjectResourceFromEntityAssembler(IOrganizationFacade orgFacade)
    {
        _orgFacade = orgFacade;
    }

    public async Task<ProjectResource> ToResourceFromEntity(Project proj)
    {
        var contractor = await _orgFacade.GetContractorByOrganizationId(proj.OrganizationId.organizationId);

        return new ProjectResource(
            proj.Id,
            proj.ProjectName.Value,
            proj.Description.Value,
            proj.DateRange.StartDate.Date,
            proj.DateRange.EndDate.Date,
            proj.Budget.Amount,
            proj.OrganizationId.organizationId,
            proj.ContractingEntityId.personId,
            contractor
        );
    }
}
