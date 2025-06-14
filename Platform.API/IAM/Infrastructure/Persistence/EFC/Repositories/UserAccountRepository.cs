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

    public async Task<UserAccount?> FindByIdWithUserTypeAsync(long id)
    {
        return await context.UserAccounts
            .Include(u => u.UserType)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<UserAccount>> ListWithUserTypeAsync()
    {
        return await context.UserAccounts
            .Include(u => u.UserType)
            .ToListAsync();
    }
    
    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Email.Address == email);
        if (person == null) return null;

        return await context.UserAccounts
            .Include(ua => ua.UserType) 
            .FirstOrDefaultAsync(ua => ua.PersonId.personId == person.Id);
    }




}