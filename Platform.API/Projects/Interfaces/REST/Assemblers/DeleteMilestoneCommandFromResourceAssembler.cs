using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class DeleteMilestoneCommandFromResourceAssembler
{
    public static DeleteMilestoneCommand ToCommandFromResource(long id)
    {
        return new DeleteMilestoneCommand(id);
    }
}