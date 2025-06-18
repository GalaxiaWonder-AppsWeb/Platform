using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.Organizations.Infrastructure.Persistence.EFC.Repositories;

public class OrganizationMemberRepository(AppDbContext context) : BaseRepository<OrganizationMember>(context), IOrganizationMemberRepository
{
    
}