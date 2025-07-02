using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

public class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    public async Task<Role?> FindByName(string name)
    {
        return await Context.Set<Role>()
            .FirstOrDefaultAsync(r => r.Name.ToString() == name);
    }
}