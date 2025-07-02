using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> FindByName(string name);
}