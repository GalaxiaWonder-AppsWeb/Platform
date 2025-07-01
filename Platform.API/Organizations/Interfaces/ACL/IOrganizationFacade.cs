namespace Platform.API.Organizations.Interfaces.ACL;

/// <summary>
/// Facade interface for accessing organization-related information.
/// </summary>
public interface IOrganizationFacade
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
    Task<long> GetContractorByOrganizationId(long organizationId);
}