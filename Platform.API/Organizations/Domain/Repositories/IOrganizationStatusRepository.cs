using Platform.API.Organizations.Domain.Model.Entities;

namespace Platform.API.Organizations.Domain.Repositories;

public interface IOrganizationStatusRepository
{
    Task<OrganizationStatus?> FindByNameAsync(string name);
}