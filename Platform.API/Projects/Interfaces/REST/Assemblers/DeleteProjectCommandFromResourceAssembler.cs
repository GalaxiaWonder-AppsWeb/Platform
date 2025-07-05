using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Interfaces.REST.Assemblers;

public class DeleteProjectCommandFromResourceAssembler
{
    public static DeleteProjectCommand ToCommandFromResource(long id)
    {
        return new DeleteProjectCommand(id);
    }
}