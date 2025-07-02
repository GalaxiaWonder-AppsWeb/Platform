using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class CreateProjectTeamMemberCommandFromResourceAssembler
{
    public static CreateProjectTeamMemberCommand ToCommandFromResource(
        long projectId, CreateProjectTeamMemberResource resource)
    {
        if (resource is null)
        {
            throw new ArgumentNullException(nameof(resource));
        }

        return new CreateProjectTeamMemberCommand(
            new ProjectId(projectId),
            new Specialty(Enum.Parse<Specialties>(resource.Specialty)),
            new OrganizationMemberId(resource.OrganizationMemberId));
    }
}