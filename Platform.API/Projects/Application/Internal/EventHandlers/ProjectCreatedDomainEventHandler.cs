using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Organizations.Interfaces.ACL;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.Events;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;
using Task = System.Threading.Tasks.Task;

namespace Platform.API.Projects.Application.Internal.EventHandlers;

public class ProjectCreatedDomainEventHandler(
    IProjectTeamMemberRepository projectTeamMemberRepository,
    ISpecialtyRepository specialtyRepository,
    IOrganizationMemberFacade organizationMemberFacade,
    IIAMContextFacade iamFacade,
    IUnitOfWork unitOfWork)
{

    public async Task Handle(ProjectCreatedDomainEvent domainEvent)
    {
        var personInformationCommand = new CreateProjectTeamMemberCommand(new ProjectId(domainEvent.ProjectId),
            new Specialty(Specialties.NON_APPLICABLE),
            new OrganizationMemberId(domainEvent.OrganizationId));
        var personId =
            organizationMemberFacade.GetPersonIdByOrganizationMemberId(
                domainEvent.OrganizationId);
        var projectTeamMember = new ProjectTeamMember(personInformationCommand);
        var personInformation = await iamFacade.GetProfileDetailsByIdAsync(personId.Result);
        if (personInformation == null)
        {
            throw new Exception($"Person with ID {personId.Result} not found");
        }
        var existingSpecialty = personInformationCommand.Specialty.Name.ToString();
        var specialty = await specialtyRepository.FindByName(existingSpecialty);
        if (specialty == null)
        {
            throw new Exception($"Specialty {personInformationCommand.Specialty.GetName()} not found");
        }
        projectTeamMember.SetSpecialty(specialty);
        projectTeamMember.SetPersonalInformation(new PersonId(personId.Result), new PersonName(personInformation.FirstName, personInformation.LastName), new EmailAddress(personInformation.Email));
        await projectTeamMemberRepository.AddAsync(projectTeamMember);
        await unitOfWork.CompleteAsync();
    }
}
