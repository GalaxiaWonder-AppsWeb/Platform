using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class CreateProjectCommandFromResourceAssembler
{
    public static CreateProjectCommand ToCommandFromResource(CreateProjectResource resource)
    {
        return new CreateProjectCommand(
            new ProjectName(resource.ProjectName),
            new Description(resource.Description),
            new DateRange(resource.StartDate, resource.EndDate),
            new OrganizationId(resource.OrganizationId),
            new PersonId(resource.ContractingEntityId),
            new ProjectStatus(ProjectStatuses.BASIC_STUDIES));
    }
}