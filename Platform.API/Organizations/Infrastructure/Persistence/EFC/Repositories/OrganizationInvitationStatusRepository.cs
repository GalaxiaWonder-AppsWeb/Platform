using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="OrganizationInvitationStatus"/> entities.
/// </summary>
public class OrganizationInvitationStatusRepository(AppDbContext context) : BaseRepository<OrganizationInvitationStatus>(context), IOrganizationInvitationStatusRepository
{
    /// <summary>
    /// Finds an <see cref="OrganizationInvitationStatus"/> by its name.
    /// </summary>
    /// <param name="name">The name of the invitation status to search for (e.g., "PENDING", "ACCEPTED", "REJECTED").</param>
    /// <returns>
    /// The matching <see cref="OrganizationInvitationStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<OrganizationInvitationStatus?> FindByNameAsync(string name)
    {
        return await Context.Set<OrganizationInvitationStatus>()
            .FirstOrDefaultAsync(oi => oi.Name.ToString().Equals(name));
    }
}