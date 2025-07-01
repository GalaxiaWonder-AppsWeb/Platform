namespace Platform.API.Organizations.Interfaces.ACL;

/// <summary>
/// Facade interface for accessing organization member information.
/// </summary>
public interface IOrganizationMemberFacade
{
    /// <summary>
    /// Retrieves the person ID associated with a given organization member ID.
    /// </summary>
    /// <param name="organizationMemberId">
    /// The unique identifier of the organization member for whom to retrieve the person ID.
    /// </param>
    /// <returns>
    /// A person id.
    /// </returns>
    Task<long> GetPersonIdByOrganizationMemberId(long organizationMemberId);
}