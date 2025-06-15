using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationRepository(AppDbContext context) : BaseRepository<Organization>(context), IOrganizationRepository
{
    public bool ExistsByRuc(string ruc)
    {
        return Context.Set<Organization>().Any(organization => organization.Ruc.Number.Equals(ruc));
    }

    public async Task<Organization?> FindByRucAsync(string ruc)
    {
        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(organization => organization.Ruc.Number.Equals(ruc));
    }

    public async Task<Organization?> FindOrganizationByMemberId(long memberId)
    {
        var organizationId = await Context.Set<OrganizationMember>()
            .Where(om => om.Id.Equals(memberId))
            .Select(om => om.OrganizationId.organizationId)
            .FirstOrDefaultAsync();

        if (organizationId.Equals(0)) return null;
        
        return await Context.Set<Organization>().FindAsync(organizationId);
    }

    public async Task<Organization?> FindOrganizationByInvitationId(long invitationId)
    {
        var organizationId = await Context.Set<OrganizationInvitation>()
            .Where(oi => oi.Id.Equals(invitationId))
            .Select(oi => oi.OrganizationId.organizationId)
            .FirstOrDefaultAsync();
        
        if (organizationId.Equals(0)) return null;
        
        return await Context.Set<Organization>().FindAsync(organizationId);
    }

    public async Task<IEnumerable<Organization>> FindOrganizationsByOrganizationMemberPersonId(long memberPersonId)
    {
        var organizationIds = await Context.Set<OrganizationMember>()
            .Where(om => om.PersonId.Equals(memberPersonId))
            .Select(om => om.OrganizationId.organizationId)
            .ToListAsync();

        return await Context.Set<Organization>()
            .Where(o => organizationIds.Contains(o.Id))
            .ToListAsync();
    }
}