using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="OrganizationInvitationStatus"/> entities.
/// </summary>
public interface IOrganizationInvitationStatusRepository: IBaseRepository<OrganizationInvitationStatus>
{
    /// <summary>
    /// Finds an <see cref="OrganizationInvitationStatus"/> by its name.
    /// </summary>
    /// <param name="name">The name of the invitation status (e.g., "PENDING", "ACCEPTED").</param>
    /// <returns>The matching <see cref="OrganizationInvitationStatus"/>, or <c>null</c> if not found.</returns>
    Task<OrganizationInvitationStatus?> FindByNameAsync(string name);
}