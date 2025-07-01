using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Interfaces.ACL;

namespace Platform.API.Organizations.Application.ACL;

public class OrganizationFacade(IOrganizationRepository organizationRepository): IOrganizationFacade
{
    
    /// <summary>
    /// Retrieves the contractor ID associated with a given organization ID.
    /// </summary>
    /// <param name="organizationId">
    /// The unique identifier of the organization for which to retrieve the contractor ID.
    /// </param>
    /// <returns>
    /// A person id.
    /// </returns>
    public async Task<long> GetContractorByOrganizationId(long organizationId)
    {
        var organization = await organizationRepository.FindByIdAsync(organizationId);
        if (organization is null)
            throw new KeyNotFoundException($"No organization found for organization ID {organizationId}.");
        return organization.CreatedBy.personId;
    }
}