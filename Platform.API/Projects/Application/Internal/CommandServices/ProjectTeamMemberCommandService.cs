using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Organizations.Interfaces.ACL;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Application.Internal.CommandServices;

public class ProjectTeamMemberCommandService(
    IProjectTeamMemberRepository projectTeamMemberRepository,
    ISpecialtyRepository specialtyRepository,
    IIAMContextFacade iamFacade,
    IOrganizationMemberFacade organizationMemberFacade,
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
        var personId =
            organizationMemberFacade.GetPersonIdByOrganizationMemberId(
                command.OrganizationMemberId.organizationMemberId);
        var projectTeamMember = new ProjectTeamMember(command);
        var personInformation = await iamFacade.GetProfileDetailsByIdAsync(personId.Result);
        if (personInformation == null)
        {
            throw new Exception($"Person with ID {personId.Result} not found");
        }
        var existingSpecialty = command.Specialty.Name.ToString();
        var specialty = await specialtyRepository.FindByName(existingSpecialty);
        if (specialty == null)
        {
            throw new Exception($"Specialty {command.Specialty.GetName()} not found");
        }
        projectTeamMember.SetSpecialty(specialty);
        projectTeamMember.SetPersonalInformation(new PersonId(personId.Result), new PersonName(personInformation.FirstName, personInformation.LastName), new EmailAddress(personInformation.Email));
        await projectTeamMemberRepository.AddAsync(projectTeamMember);
        await unitOfWork.CompleteAsync();
        return projectTeamMember;
    }
}