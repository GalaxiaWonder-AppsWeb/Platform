using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationMemberTypeRepository(AppDbContext context): BaseRepository<OrganizationMemberType>(context), IOrganizationMemberTypeRepository
{
    public async Task<OrganizationMemberType?> FindByNameAsync(string name)
    {
        return await Context.Set<OrganizationMemberType>()
            .FirstOrDefaultAsync(ut => ut.Name.ToString() == name);
    }
}