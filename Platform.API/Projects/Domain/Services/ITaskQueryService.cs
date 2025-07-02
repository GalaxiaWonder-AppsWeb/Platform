using Platform.API.Projects.Domain.Model.Queries;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Domain.Services;

public interface ITaskQueryService
{
    Task<IEnumerable<Task>> Handle(GetAllTasksByPersonIdAndMilestoneIdQuery query);
}