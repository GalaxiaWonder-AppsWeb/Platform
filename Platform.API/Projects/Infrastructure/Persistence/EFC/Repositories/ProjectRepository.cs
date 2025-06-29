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
    
    public async Task<IEnumerable<Project>> FindAllProjectsByTeamMemberPersonIdAsync(long personId)
    {
        var projects = await context.Projects
            .Join(
                context.ProjectTeamMembers,
                project => project.Id,
                member => member.ProjectId.Value,
                (project, member) => new { project, member }
            )
            .Where(x => x.member.PersonId.personId == personId)
            .Select(x => x.project)
            .Distinct()
            .ToListAsync();

        return projects;
    }





}