using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Domain.Repositories;

public interface IChangeProcessStatusRepository : IBaseRepository<ChangeProcessStatus>
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
    Task<ChangeProcessStatus?> FindByName(string name);
}