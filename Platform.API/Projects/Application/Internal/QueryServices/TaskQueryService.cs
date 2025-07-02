using Platform.API.Projects.Domain.Model.Queries;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Application.Internal.QueryServices;

public class TaskQueryService(
    ITaskRepository taskRepository
    ): ITaskQueryService
{
    public async Task<IEnumerable<Task>> Handle(GetAllTasksByMilestoneIdQuery query)
    {
        return await taskRepository.FindAllTasksByMilestoneId(query.MilestoneId);
    }
    
    public async Task<IEnumerable<Task>> Handle(GetAllTasksByPersonIdAndMilestoneIdQuery query)
    {
        return await taskRepository.FindAllTasksByPersonIdAndMilestoneId(query.PersonId, query.MilestoneId);
    }
}