using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationMemberRepository(AppDbContext context) : BaseRepository<OrganizationMember>(context), IOrganizationMemberRepository
{
    public async Task<IEnumerable<OrganizationMember>> FindMembersByOrganizationId(long organizationId)
    {
        return await context.Set<OrganizationMember>()
            .Include(m => m.MemberType)
            .Where(m => m.OrganizationId.organizationId == organizationId)
            .ToListAsync();
    }

}