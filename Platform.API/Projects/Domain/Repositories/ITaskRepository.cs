using Platform.API.Shared.Domain.Repositories;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Domain.Repositories;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<Task?> FindById(long id);
    Task<IEnumerable<Task>> FindAllTasksByMilestoneId(long milestoneId);
    Task<IEnumerable<Task>> FindAllTasksByPersonIdAndMilestoneId(long personId, long milestoneId);
    Task<IEnumerable<Task>> FindAllTasksByMilestoneIds(IEnumerable<long> milestoneIds);
}