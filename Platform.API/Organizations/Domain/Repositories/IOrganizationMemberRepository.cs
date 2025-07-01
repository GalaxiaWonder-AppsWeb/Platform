using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="OrganizationMember"/> entities.
/// </summary>
public interface IOrganizationMemberRepository : IBaseRepository<OrganizationMember>
{
    /// <summary>
    /// Finds an <see cref="OrganizationMember"/> by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the organization member.</param>
    /// <returns>The <see cref="OrganizationMember"/> if found; otherwise, <c>null</c>.</returns>
    new Task<OrganizationMember?> FindByIdAsync(long id);

    /// <summary>
    /// Retrieves all members associated with a specific organization.
    /// </summary>
    /// <param name="organizationId">The unique identifier of the organization.</param>
    /// <returns>A collection of <see cref="OrganizationMember"/> entities.</returns>
    Task<IEnumerable<OrganizationMember>> FindMembersByOrganizationId(long organizationId);

    /// <summary>
    /// Retrieves a person id by the organization member id.
    /// </summary>
    /// <param name="organizationMemberId">
    /// The unique identifier of the organization member.
    /// </param>
    /// <returns>
    /// The person id associated with the organization member, or <c>null</c> if not found.
    /// </returns>
    Task<long?> FindPersonIdByOrganizationMemberId(long organizationMemberId);
}