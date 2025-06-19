using Platform.API.Organizations.Domain.Model.Entities;

namespace Platform.API.Organizations.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="OrganizationStatus"/> entities.
/// </summary>
public interface IOrganizationStatusRepository
{
    /// <summary>
    /// Finds an <see cref="OrganizationStatus"/> by its name.
    /// </summary>
    /// <param name="name">The name of the organization status (e.g., ACTIVE, INACTIVE).</param>
    /// <returns>The <see cref="OrganizationStatus"/> if found; otherwise, <c>null</c>.</returns>
    Task<OrganizationStatus?> FindByNameAsync(string name);
}