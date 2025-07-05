namespace Platform.API.Projects.Interfaces.REST.Resources;

public record CreateProjectTeamMemberResource(string Role, string Specialty, long OrganizationMemberId);