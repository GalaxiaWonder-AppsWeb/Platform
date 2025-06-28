using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Domain.Services;

/// <summary>
/// Defines the contract for handling commands related to project team members.
/// </summary>
public interface IProjectTeamMemberCommandService
{
    /// <summary>
    /// Handles the creation of a new project team member.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create the project team member, including project ID, specialty, organization member ID, person ID, person name, and email address.
    /// </param>
    /// <returns>
    /// The newly created <see cref="ProjectTeamMember"/> entity, or null if the creation failed.
    /// </returns>
    Task<ProjectTeamMember?> Handle(CreateProjectTeamMemberCommand command);
}