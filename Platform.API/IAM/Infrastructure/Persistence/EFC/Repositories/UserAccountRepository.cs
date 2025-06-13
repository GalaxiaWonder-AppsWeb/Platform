using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;

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
    
    public new async Task<UserAccount?> FindByEmailAsync(string email)
    {
        var person = await context.Set<Person>()
            .FirstOrDefaultAsync(p => p.Email.Address == email);

        if (person is null) return null;

        return await context.Set<UserAccount>()
            .FirstOrDefaultAsync(u => u.PersonId.personId == person.Id);
    }
}