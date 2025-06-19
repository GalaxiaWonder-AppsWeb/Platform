using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="OrganizationMemberType"/> entities.
/// </summary>
public interface IOrganizationMemberTypeRepository: IBaseRepository<OrganizationMemberType>
{
    /// <summary>
    /// Finds an <see cref="OrganizationMemberType"/> by its name.
    /// </summary>
    /// <param name="name">The name of the member type (e.g., CONTRACTOR, WORKER).</param>
    /// <returns>The <see cref="OrganizationMemberType"/> if found; otherwise, <c>null</c>.</returns>
    Task<OrganizationMemberType?> FindByNameAsync(string name);
}