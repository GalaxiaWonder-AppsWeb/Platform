using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="Specialty"/> entities.
/// </summary>
/// <param name="context">
/// The database context used for accessing the <see cref="Specialty"/> entities.
/// </param>
public class SpecialtyRepository(AppDbContext context) : BaseRepository<Specialty>(context), ISpecialtyRepository
{
    /// <summary>
    /// Finds a <see cref="Specialty"/> by its name.
    /// </summary>
    /// <param name="name">The name of the specialty (e.g., "ARCHITECTURE, STRUCTURES, HSA, ETC").</param>
    /// <returns>The matching <see cref="Specialty"/> if found; otherwise, <c>null</c>.</returns>
    public async Task<Specialty?> FindByName(string name)
    {
        return await Context.Set<Specialty>()
            .FirstOrDefaultAsync(s => s.Name.ToString() == name);
    }
}