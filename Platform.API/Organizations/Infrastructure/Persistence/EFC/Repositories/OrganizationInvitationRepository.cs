using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationInvitationRepository(AppDbContext context) :  BaseRepository<OrganizationInvitation>(context), IOrganizationInvitationRepository
{
    public async Task<IEnumerable<OrganizationInvitation>> FindInvitationsByMemberPersonId(long personId)
    {
        return await context.OrganizationInvitations
            .Where(i => i.PersonId.personId == personId)
            .OrderByDescending(i => i.CreatedDate)
            .ToListAsync();
    }

    public async Task<OrganizationInvitation?> FindLatestInvitation(long organizationId, long personId)
    {
        return await context.OrganizationInvitations
            .Where(i => i.OrganizationId.organizationId == organizationId && i.PersonId.personId == personId)
            .OrderByDescending(i => i.CreatedDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrganizationInvitation>> FindInvitationsByOrganizationId(long id)
    {
        return await context.Set<OrganizationInvitation>()
            .Include(i => i.Status)
            .Where(i => i.OrganizationId.organizationId == id)
            .ToListAsync();
    }

}