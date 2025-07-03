using Microsoft.EntityFrameworkCore;
using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Change.Infrastructure.Persistence.EFC.Repositories;

public class ChangeOriginRepository(AppDbContext context) : BaseRepository<ChangeOrigin>(context), IChangeOriginRepository
{
    /// <summary>
    /// Find an <see cref="ChangeOrigin"/> by its name.
    /// </summary>
    /// <param name="name">
    /// The name of the status as a string.
    /// </param>
    /// <returns>
    /// The matching <see cref="ChangeOrigin"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<ChangeOrigin?> FindByName(string name)
    {
        return await Context.Set<ChangeOrigin>()
            .FirstOrDefaultAsync(co => co.Name.ToString() == name);
    }
}