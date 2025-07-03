using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Interfaces.ACL;

namespace Platform.API.Projects.Application.ACL;

public class ProjectFacade(
    IMilestoneRepository milestoneRepository,
    ITaskRepository taskRepository) : IProjectFacade
{
    public async Task<IEnumerable<long>> GetTaskIdsByProjectId(long projectId)
    {
        var milestones = await milestoneRepository.FindAllMilestonesByProjectId(projectId);
        var milestoneIds = milestones.Select(m => m.Id).ToList();
        if (!milestoneIds.Any())
        {
            return Enumerable.Empty<long>();
        }

        var tasks = await taskRepository.FindAllTasksByMilestoneIds(milestoneIds);
        return tasks.Select(t => t.Id);
    }
}