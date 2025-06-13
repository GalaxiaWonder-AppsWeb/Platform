using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserTypeRepository(AppDbContext context)
    : BaseRepository<UserType>(context), IUserTypeRepository
{
    public bool ExistsByName(string name)
    {
        return Context.Set<UserType>().Any(ut => ut.Name.ToString() == name);
    }

    public async Task<UserType?> FindByNameAsync(string name)
    {
        return await Context.Set<UserType>()
            .FirstOrDefaultAsync(ut => ut.Name.ToString() == name);
    }
}