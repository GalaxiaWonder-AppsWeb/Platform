using Platform.API.Organizations.Domain.Repositories;
using Platform.API.Organizations.Interfaces.ACL;

namespace Platform.API.Organizations.Application.ACL;

public class OrganizationMemberFacade(IOrganizationMemberRepository organizationMemberRepository) : IOrganizationMemberFacade
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
 public async Task<long> GetPersonIdByOrganizationMemberId(long organizationMemberId)
 {
  var personId = await organizationMemberRepository.FindPersonIdByOrganizationMemberId(organizationMemberId);
  if (personId is null)
   throw new KeyNotFoundException($"No person found for organization member ID {organizationMemberId}.");
  
  return personId.Value;
 }
   
}