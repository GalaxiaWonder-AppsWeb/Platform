using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeOriginRepository : IBaseRepository<ChangeOrigin>
{
    Task<ChangeOrigin?> FindByName(string name);
}