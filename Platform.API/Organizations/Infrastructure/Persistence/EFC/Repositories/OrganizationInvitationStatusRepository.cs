using Microsoft.EntityFrameworkCore;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationInvitationStatusRepository(AppDbContext context) : BaseRepository<OrganizationInvitationStatus>(context), IOrganizationInvitationStatusRepository
{
    public async Task<OrganizationInvitationStatus?> FindByNameAsync(string name)
    {
        return await Context.Set<OrganizationInvitationStatus>()
            .FirstOrDefaultAsync(oi => oi.Name.ToString().Equals(name));
    }
}