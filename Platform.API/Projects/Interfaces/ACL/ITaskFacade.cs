namespace Platform.API.Projects.Interfaces.ACL;

public interface ITaskFacade
{
    Task<bool> TaskExists(long taskId);
}