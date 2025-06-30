namespace Platform.API.Projects.Interfaces.REST.Resources;

public record CreateProjectTeamMemberResource(string Specialty, long OrganizationMemberId, long PersonId, string FirstName, string LastName, string EmailAddress);