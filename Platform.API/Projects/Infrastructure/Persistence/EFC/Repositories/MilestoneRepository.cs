using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

public class MilestoneRepository(AppDbContext context) : BaseRepository<Milestone>(context), IMilestoneRepository
{
    public async Task<Milestone?> FindById(long id)
    {
        return await Context.Set<Milestone>()
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<IEnumerable<Milestone>> FindAllMilestonesByProjectId(long projectId)
    {
        return await Context.Set<Milestone>()
            .Where(m => m.ProjectId.Value == projectId)
            .ToListAsync();
    }
}