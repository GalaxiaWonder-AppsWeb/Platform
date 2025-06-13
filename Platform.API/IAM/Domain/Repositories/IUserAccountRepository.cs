using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

public interface IUserAccountRepository : IBaseRepository<UserAccount>
{
    bool ExistsByUserName(UserName userName);
    
    Task<UserAccount?> FindByUserNameAsync(UserName userName);
}