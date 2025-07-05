using Microsoft.EntityFrameworkCore;
using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Change.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Change.Infrastructure.Persistence.EFC.Repositories;

public class ChangeProcessStatusRepository(AppDbContext context) : BaseRepository<ChangeProcessStatus>(context), IChangeProcessStatusRepository
{
    /// <summary>
    /// Find an <see cref="ChangeProcessStatus"/> by its name.
    /// </summary>
    /// <param name="name">
    /// The name of the status as a <see cref="ChangeProcessStatuses"/>.
    /// </param>
    /// <returns>
    /// The matching <see cref="ChangeProcessStatus"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ChangeProcessStatus?> FindByName(string name)
    {
        return await Context.Set<ChangeProcessStatus>()
            .FirstOrDefaultAsync(cps => cps.Name.ToString() == name);
    }
}