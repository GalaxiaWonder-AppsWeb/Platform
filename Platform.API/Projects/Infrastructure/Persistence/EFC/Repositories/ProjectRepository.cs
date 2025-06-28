using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;


public class ProjectRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository
{
    public async Task<Project?> FindById(long id)
    {
        return await Context.Set<Project>()
            .Include(p => p.OrganizationId)
            .Include(p => p.ContractingEntityId)
            .Include(p => p.Status)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}