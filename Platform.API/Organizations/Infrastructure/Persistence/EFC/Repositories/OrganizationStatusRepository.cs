using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="OrganizationStatus"/> entities.
/// </summary>
public class OrganizationStatusRepository(AppDbContext context): BaseRepository<OrganizationStatus>(context), IOrganizationStatusRepository
{
    /// <summary>
    /// Finds an <see cref="OrganizationStatus"/> by its name.
    /// </summary>
    /// <param name="name">The name of the status as a string (e.g., "ACTIVE", "INACTIVE").</param>
    /// <returns>
    /// The matching <see cref="OrganizationStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<OrganizationStatus?> FindByNameAsync(string name)
    {
        return await Context.Set<OrganizationStatus>()
            .FirstOrDefaultAsync(ut => ut.Name.ToString() == name);
    }
}