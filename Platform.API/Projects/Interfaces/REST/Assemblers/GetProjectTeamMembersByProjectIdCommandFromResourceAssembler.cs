using Platform.API.Projects.Domain.Model.Queries;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class GetProjectTeamMembersByProjectIdCommandFromResourceAssembler
{
    public static GetAllProjectTeamMembersByProjectIdQuery 
        ToCommandFromResource(long projectId)
    {
        return new GetAllProjectTeamMembersByProjectIdQuery(projectId);
    }
}