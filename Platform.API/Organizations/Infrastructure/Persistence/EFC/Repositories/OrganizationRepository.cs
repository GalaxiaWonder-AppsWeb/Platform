using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationRepository(AppDbContext context) : BaseRepository<Organization>(context), IOrganizationRepository
{
    
    public new async Task<Organization?> FindByIdAsync(long id)
    {
        var organization = await context.Organizations
            .Include(o => o.Status)
            .Include(o => o.Ruc)
            .Include(o => o.LegalName)
            .Include(o => o.CommercialName)
            .Include(o => o.CreatedBy)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (organization == null) return null;

        // Poblar los IDs manualmente
        var memberIds = await Context.Set<OrganizationMember>()
            .Where(m => m.OrganizationId.organizationId == organization.Id)
            .Select(m => m.Id)
            .ToListAsync();

        var invitationIds = await Context.Set<OrganizationInvitation>()
            .Where(i => i.OrganizationId.organizationId == organization.Id)
            .Select(i => i.Id)
            .ToListAsync();

        organization.SetMemberIds(memberIds);
        organization.SetInvitationIds(invitationIds);

        return organization;
    }
    
    public bool ExistsByRuc(string ruc)
    {
        return Context.Set<Organization>().Any(organization => organization.Ruc.Number.Equals(ruc));
    }

    public bool ExistsById(long id)
    {
        return Context.Set<Organization>().Any(organization => organization.Id.Equals(id));
    }

    public async Task<Organization?> FindByRucAsync(string ruc)
    {
        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(organization => organization.Ruc.Number.Equals(ruc));
    }

    public async Task<Organization?> FindByInvitationIdAsync(long invitationId)
    {
        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(organization => organization.Id.Equals(invitationId));
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
            .Where(m => m.PersonId.personId == memberPersonId)
            .Select(m => m.OrganizationId.organizationId)
            .Distinct()
            .ToListAsync();

        var organizations = await Context.Organizations
            .Where(o => organizationIds.Contains(o.Id))
            .Include(o => o.Status)
            .Include(o => o.Ruc)
            .Include(o => o.LegalName)
            .Include(o => o.CommercialName)
            .Include(o => o.CreatedBy)
            .ToListAsync();

        foreach (var org in organizations)
        {
            var memberIds = await Context.Set<OrganizationMember>()
                .Where(m => m.OrganizationId.organizationId == org.Id)
                .Select(m => m.Id)
                .ToListAsync();

            var invitationIds = await Context.Set<OrganizationInvitation>()
                .Where(i => i.OrganizationId.organizationId == org.Id)
                .Select(i => i.Id)
                .ToListAsync();

            org.SetMemberIds(memberIds);
            org.SetInvitationIds(invitationIds);
        }

        return organizations;
    }


}