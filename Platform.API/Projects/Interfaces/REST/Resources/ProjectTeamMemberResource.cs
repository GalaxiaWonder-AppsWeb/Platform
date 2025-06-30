namespace Platform.API.Projects.Interfaces.REST.Resources;

public record ProjectTeamMemberResource(long Id, string Specialty, long OrganizationMemberId, long PersonId, string FirstName, string LastName, string EmailAddress);