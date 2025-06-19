using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="OrganizationMemberType"/> entities.
/// </summary>
public class OrganizationMemberTypeRepository(AppDbContext context): BaseRepository<OrganizationMemberType>(context), IOrganizationMemberTypeRepository
{
    /// <summary>
    /// Finds an <see cref="OrganizationMemberType"/> by its name.
    /// </summary>
    /// <param name="name">The name of the member type as a string (e.g., "CONTRACTOR", "WORKER").</param>
    /// <returns>
    /// The matching <see cref="OrganizationMemberType"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<OrganizationMemberType?> FindByNameAsync(string name)
    {
        return await Context.Set<OrganizationMemberType>()
            .FirstOrDefaultAsync(ut => ut.Name.ToString() == name);
    }
}