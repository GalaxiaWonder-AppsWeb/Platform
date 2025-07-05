using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeProcessRepository : IBaseRepository<ChangeProcess>
{
    /// <summary>
    /// Find an existing <see cref="ChangeProcess"/> by its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the change process.
    /// </param>
    /// <returns>
    /// The matching <see cref="ChangeProcess"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ChangeProcess?> FindById(long id);
    
    /// <summary>
    /// Find an existing <see cref="ChangeProcess"/> by his linked project.
    /// </summary>
    /// <param name="projectId">
    /// The unique identifier of the project associated with the change process.
    /// </param>
    /// <returns>
    /// The matching <see cref="ChangeProcess"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ChangeProcess?> FindByProjectId(long projectId);
}