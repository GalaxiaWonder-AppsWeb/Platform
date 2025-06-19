using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Organizations.Domain.Repositories;

/// <summary>
/// Defines the contract for repository operations related to <see cref="Organization"/> entities.
/// </summary>
public interface IOrganizationRepository : IBaseRepository<Organization>
{
    /// <summary>
    /// Finds an <see cref="Organization"/> by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the organization.</param>
    /// <returns>The <see cref="Organization"/> if found; otherwise, <c>null</c>.</returns>
    new Task<Organization?> FindByIdAsync(long id);
    
    /// <summary>
    /// Finds the <see cref="Organization"/> associated with the specified invitation ID.
    /// </summary>
    /// <param name="invitationId">The unique identifier of the invitation.</param>
    /// <returns>The corresponding <see cref="Organization"/> if found; otherwise, <c>null</c>.</returns>
    Task<Organization?> FindByInvitationIdAsync(long invitationId);
    
    /// <summary>
    /// Checks whether an organization exists with the specified RUC.
    /// </summary>
    /// <param name="ruc">The RUC (Registro Único de Contribuyentes) number.</param>
    /// <returns><c>true</c> if an organization exists with the given RUC; otherwise, <c>false</c>.</returns>
    bool ExistsByRuc(string ruc);
    
    /// <summary>
    /// Checks whether an organization exists with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the organization.</param>
    /// <returns><c>true</c> if an organization exists with the given ID; otherwise, <c>false</c>.</returns>
    bool ExistsById(long id);
    
    /// <summary>
    /// Finds an <see cref="Organization"/> by its RUC.
    /// </summary>
    /// <param name="ruc">The RUC (Registro Único de Contribuyentes) number.</param>
    /// <returns>The <see cref="Organization"/> if found; otherwise, <c>null</c>.</returns>
    Task<Organization?> FindByRucAsync(string ruc);

    /// <summary>
    /// Finds the <see cref="Organization"/> associated with a specific member ID.
    /// </summary>
    /// <param name="memberId">The unique identifier of the organization member.</param>
    /// <returns>The corresponding <see cref="Organization"/> if found; otherwise, <c>null</c>.</returns>
    Task<Organization?> FindOrganizationByMemberId(long memberId);
    
    /// <summary>
    /// Finds the <see cref="Organization"/> associated with a specific invitation ID.
    /// </summary>
    /// <param name="invitationId">The unique identifier of the invitation.</param>
    /// <returns>The corresponding <see cref="Organization"/> if found; otherwise, <c>null</c>.</returns>
    Task<Organization?> FindOrganizationByInvitationId(long invitationId);
    
    /// <summary>
    /// Retrieves all <see cref="Organization"/> entities in which a person is registered as a member.
    /// </summary>
    /// <param name="memberPersonId">The unique identifier of the person.</param>
    /// <returns>A collection of <see cref="Organization"/> entities.</returns>
    Task<IEnumerable<Organization>> FindOrganizationsByOrganizationMemberPersonId(long memberPersonId);
}