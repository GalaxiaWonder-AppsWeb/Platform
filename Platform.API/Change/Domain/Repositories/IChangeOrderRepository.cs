using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeOrderRepository : IBaseRepository<ChangeOrder>
{
    Task<ChangeOrder?> FindById(long id);
    
    Task<ChangeOrder?> FindByChangeProcessId(long changeProcessId);
}