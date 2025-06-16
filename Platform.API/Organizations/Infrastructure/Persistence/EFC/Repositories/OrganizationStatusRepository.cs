using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationStatusRepository(AppDbContext context): BaseRepository<OrganizationStatus>(context), IOrganizationStatusRepository
{
    public async Task<OrganizationStatus?> FindByNameAsync(string name)
    {
        return await Context.Set<OrganizationStatus>()
            .FirstOrDefaultAsync(ut => ut.Name.ToString() == name);
    }
}