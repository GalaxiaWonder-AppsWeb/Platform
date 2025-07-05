using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Interfaces.REST.Resources;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class ProjectTeamMemberResourceFromEntityAssembler
{
    public static ProjectTeamMemberResource ToResourceFromEntity(ProjectTeamMember projectTeamMember)
    {
        return new ProjectTeamMemberResource(
            projectTeamMember.Id,
            projectTeamMember.Role.Name.ToString(),
            projectTeamMember.Specialty.Name.ToString(),
            projectTeamMember.OrganizationMemberId.organizationMemberId,
            projectTeamMember.PersonId.personId,
            projectTeamMember.PersonName.FirstName,
            projectTeamMember.PersonName.LastName,
            projectTeamMember.EmailAddress.Address);
    }
}