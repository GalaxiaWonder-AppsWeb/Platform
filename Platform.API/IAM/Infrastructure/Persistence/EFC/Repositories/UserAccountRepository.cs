using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserAccountRepository(AppDbContext context) : BaseRepository<UserAccount>(context), IUserAccountRepository
{
    public bool ExistsByUserName(UserName userName)
    {
        return Context.Set<UserAccount>()
            .Any(userAccount => userAccount.Username.Username.Equals(userName.Username));
    }

    public async Task<UserAccount?> FindByUserNameAsync(UserName userName)
    {
        return await Context.Set<UserAccount>()
            .FirstOrDefaultAsync(userAccount => userAccount.Username.ToString().Equals(userName.ToString()));
    }
    
    public async Task<UserAccount?> FindByNameAsync(UserTypes name)
    {
        return await Context.Set<UserAccount>()
            .FirstOrDefaultAsync(user => user.UserType == name);
    }

    public bool ExistsByName(UserTypes name)
    {
        return Context.Set<UserAccount>().Any(user => user.UserType == name);
    }
}