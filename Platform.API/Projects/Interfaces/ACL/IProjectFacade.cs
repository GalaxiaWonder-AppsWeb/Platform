namespace Platform.API.Projects.Interfaces.ACL;

public interface IProjectFacade
{
    Task<IEnumerable<long>> GetTaskIdsByProjectId(long projectId);
    
    Task<bool> ProjectExists(long projectId);
}