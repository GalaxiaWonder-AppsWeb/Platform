namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// Command to delete a team member from a project.
/// </summary>
/// <param name="Id">
/// Person id of the team member to be deleted.
/// </param>
public record DeleteProjectTeamMemberCommand(long Id);