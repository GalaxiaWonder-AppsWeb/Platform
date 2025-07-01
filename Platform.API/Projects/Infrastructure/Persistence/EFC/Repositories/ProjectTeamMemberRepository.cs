using Microsoft.EntityFrameworkCore;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Projects.Infrastructure.Persistence.EFC.Repositories;

/// <inheritdoc cref="IProjectTeamMemberRepository"/>
public class ProjectTeamMemberRepository(AppDbContext context): BaseRepository<ProjectTeamMember>(context), IProjectTeamMemberRepository
{
    /// <inheritdoc cref="FindById"/>
    public async Task<ProjectTeamMember?> FindById(long id)
    {
        return await Context.Set<ProjectTeamMember>()
            .FirstOrDefaultAsync(ptm => ptm.Id == id);
    }
}