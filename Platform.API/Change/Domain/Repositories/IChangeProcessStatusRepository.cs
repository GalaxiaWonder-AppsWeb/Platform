using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeProcessStatusRepository : IBaseRepository<ChangeProcessStatus>
{
    Task<ChangeProcessStatus?> FindByName(string name);
}