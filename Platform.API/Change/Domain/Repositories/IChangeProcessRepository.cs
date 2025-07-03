using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeProcessRepository : IBaseRepository<ChangeProcess>
{
    Task<ChangeProcess?> FindById(long id);
    
    Task<ChangeProcess?> FindByProjectId(long projectId);
}