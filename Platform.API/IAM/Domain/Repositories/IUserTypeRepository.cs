using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

public interface IUserTypeRepository : IBaseRepository<UserType> 
{
    Task<UserType?> FindByNameAsync(string name);
    bool ExistsByName(string name);
}