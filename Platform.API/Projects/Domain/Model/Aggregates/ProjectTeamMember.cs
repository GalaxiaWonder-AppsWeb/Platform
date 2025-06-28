using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Aggregates;

/// <summary>
/// Class representing a member of a project team.
/// Wraps the properties and behaviors related to a team member within a project.
/// Persists the member's details such as their role, organization membership, and contact information.
/// </summary>
public partial class ProjectTeamMember
{
    /// <summary>
    /// Represents the unique identifier for the project.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Identifier of the project to which this team member belongs, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public ProjectId ProjectId { get; private set; }
    
    /// <summary>
    /// Represents the specialty of the team member, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public Specialty Specialty { get; private set; }
    
    /// <summary>
    /// Identifier of the organization member, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public OrganizationMemberId OrganizationMemberId { get; private set; }
    
    /// <summary>
    /// Identifier of the person associated with this team member, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public PersonId PersonId { get; private set; }
    
    /// <summary>
    /// Name of the person associated with this team member, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public PersonName PersonName { get; private set; }
    
    /// <summary>
    /// Email address of the team member, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public EmailAddress EmailAddress { get; private set; }

    /// <summary>
    /// Default constructor for the <see cref="ProjectTeamMember"/> class.
    /// </summary>
    public ProjectTeamMember(){}
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectTeamMember"/> class with the specified command.
    /// </summary>
    /// <param name="command">
    /// The command containing the necessary data to create a project team member.
    /// </param>
    public ProjectTeamMember(CreateProjectTeamMemberCommand command)
    {
        ProjectId = command.ProjectId;
        OrganizationMemberId = command.OrganizationMemberId;
        PersonId = command.PersonId;
        PersonName = command.PersonName;
        EmailAddress = command.EmailAddress;
    }
    
    /// <summary>
    /// Set the specialty of the project team member.
    /// </summary>
    /// <param name="specialty">
    /// The new specialty to be assigned to the team member.
    /// </param>
    public void SetSpecialty(Specialty specialty)
    {
        Specialty = specialty;
    }
}