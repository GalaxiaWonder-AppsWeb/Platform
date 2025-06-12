using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

public interface IUserAccountRepository : IBaseRepository<UserAccount>
{
    bool existsByUserName(UserName userName);
    
    Task<UserAccount?> findByUserName(UserName userName);
}