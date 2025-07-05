using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class DeleteTaskCommandFromResourceAssembler
{
    public static DeleteTaskCommand ToCommandFromResource(long id)
    {
        return new DeleteTaskCommand(id);
    }
}