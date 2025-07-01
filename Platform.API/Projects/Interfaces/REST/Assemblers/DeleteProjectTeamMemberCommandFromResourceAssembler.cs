using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class DeleteProjectTeamMemberCommandFromResourceAssembler
{
    public static DeleteProjectTeamMemberCommand ToCommandFromResource(long id)
    {
        return new DeleteProjectTeamMemberCommand(id);
    }
}