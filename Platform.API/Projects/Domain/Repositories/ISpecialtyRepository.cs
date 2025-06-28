using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="Specialty"/> entities.
/// </summary>
public interface ISpecialtyRepository: IBaseRepository<Specialty>
{
    /// <summary>
    /// Finds a <see cref="Specialty"/> by its name.
    /// </summary>
    /// <param name="name">
    /// The name of the specialty (e.g., "ARCHITECTURE, STRUCTURES, HSA, ETC").
    /// </param>
    /// <returns></returns>
    Task<Specialty?> FindByName(string name);
}