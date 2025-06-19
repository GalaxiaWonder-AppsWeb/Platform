using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for accessing <see cref="OrganizationMember"/> entities.
/// </summary>
public class OrganizationMemberRepository(AppDbContext context) : BaseRepository<OrganizationMember>(context), IOrganizationMemberRepository
{
    /// <summary>
    /// Finds an <see cref="OrganizationMember"/> by its unique identifier, including its member type.
    /// </summary>
    /// <param name="id">The unique identifier of the organization member.</param>
    /// <returns>
    /// The <see cref="OrganizationMember"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public new async Task<OrganizationMember?> FindByIdAsync(long id)
    {
        return await context.Set<OrganizationMember>()
            .Include(m => m.MemberType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    /// <summary>
    /// Retrieves all <see cref="OrganizationMember"/>s associated with a specific organization.
    /// </summary>
    /// <param name="organizationId">The ID of the organization.</param>
    /// <returns>
    /// A list of members belonging to the specified organization, including their member type.
    /// </returns>
    public async Task<IEnumerable<OrganizationMember>> FindMembersByOrganizationId(long organizationId)
    {
        return await context.Set<OrganizationMember>()
            .Include(m => m.MemberType)
            .Where(m => m.OrganizationId.organizationId == organizationId)
            .ToListAsync();
    }

}