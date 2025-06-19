using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationInvitationRepository(AppDbContext context) :  BaseRepository<OrganizationInvitation>(context), IOrganizationInvitationRepository
{
    public new async Task<OrganizationInvitation?> FindByIdAsync(long id)
    {
        return await context.Set<OrganizationInvitation>()
            .Include(i => i.Status)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
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
    public async Task<IEnumerable<(OrganizationInvitation, Organization, Person)>> FindAllInvitationsWithDetailsByPersonIdAsync(long personId)
    {
        // 1. Obtener todas las invitaciones hechas a la persona
        var invitations = await context.Set<OrganizationInvitation>()
            .Include(i => i.Status)
            .Where(i => i.PersonId.personId == personId)
            .ToListAsync();

        if (!invitations.Any()) return Enumerable.Empty<(OrganizationInvitation, Organization, Person)>();

        // 2. Extraer IDs únicos de organizaciones e invitados
        var organizationIds = invitations.Select(i => i.OrganizationId.organizationId).Distinct().ToList();
        var inviterIds = invitations.Select(i => i.InvitedBy.personId).Distinct().ToList();

        // 3. Cargar organizaciones e invitados en diccionarios para acceso rápido
        var organizations = await context.Set<Organization>()
            .Where(o => organizationIds.Contains(o.Id))
            .ToDictionaryAsync(o => o.Id);

        var people = await context.Set<Person>()
            .Where(p => inviterIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        // 4. Mapear a tuplas válidas
        var result = invitations
            .Where(i => organizations.ContainsKey(i.OrganizationId.organizationId) && people.ContainsKey(i.InvitedBy.personId))
            .Select(i =>
            {
                var organization = organizations[i.OrganizationId.organizationId];
                var invitedBy = people[i.InvitedBy.personId];

                // Establecer referencias (opcional, si las usas en el assembler)
                i.SetOrganization(new OrganizationId(organization.Id));
                i.SetInvitedBy(new PersonId(invitedBy.Id));

                return (i, organization, invitedBy);
            })
            .ToList();

        return result;
    }




}