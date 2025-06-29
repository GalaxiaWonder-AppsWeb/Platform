using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Application.Internal.CommandServices;

public class ProjectTeamMemberCommandService(
    IProjectTeamMemberRepository projectTeamMemberRepository,
    ISpecialtyRepository specialtyRepository,
    IUnitOfWork unitOfWork) : IProjectTeamMemberCommandService
{
    /// <summary>
    /// Handles the creation of a new project team member.
    /// </summary>
    /// <param name="command">
    /// The command containing project team member creation details.
    /// </param>
    /// <returns>
    /// The newly created project team member or null if creation failed.
    /// </returns>
    /// <exception cref="Exception">
    /// Throws an exception if the specialty is not found in the repository.
    /// </exception>
    public async Task<ProjectTeamMember?> Handle(CreateProjectTeamMemberCommand command)
    {
        var projectTeamMember = new ProjectTeamMember(command);
        var existingSpecialty = command.Specialty.Name.ToString();
        var specialty = await specialtyRepository.FindByName(existingSpecialty);
        if (specialty == null)
        {
            throw new Exception($"Specialty {command.Specialty.GetName()} not found");
        }
        projectTeamMember.SetSpecialty(specialty);
        await projectTeamMemberRepository.AddAsync(projectTeamMember);
        await unitOfWork.CompleteAsync();
        return projectTeamMember;
    }
}