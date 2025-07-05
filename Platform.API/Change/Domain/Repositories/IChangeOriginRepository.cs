using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeOriginRepository : IBaseRepository<ChangeOrigin>
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
    Task<ChangeOrigin?> FindByName(string name);
}