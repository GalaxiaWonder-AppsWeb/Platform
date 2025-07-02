using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="Milestone"/> entities.
/// </summary>
public interface IMilestoneRepository : IBaseRepository<Milestone>
{
    Task<Milestone?> FindById(long id);
    Task<IEnumerable<Milestone>> FindAllMilestonesByProjectId(long projectId);
}