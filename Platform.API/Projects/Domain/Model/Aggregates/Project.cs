using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Aggregates;

/// <summary>
/// 
/// </summary>
public partial class Project
{
    /// <summary>
    /// Represents the unique identifier for the project.
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Name of the project, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public ProjectName ProjectName { get; set; }
    
    /// <summary>
    /// Description of the project, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public Description Description { get; set; }
    
    /// <summary>
    /// Range of dates within the project will be done.
    /// </summary>
    public DateRange DateRange { get; set; }
    
    /// <summary>
    /// Identifier of the organization that owns the project, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public OrganizationId OrganizationId { get; set; }
    
    /// <summary>
    /// Identifier of the person or entity in charge of contracting.
    /// </summary>
    public PersonId ContractingEntityId { get; set; }
    
    /// <summary>
    /// Represents the current status of the project, encapsulated in a value object to ensure validation and immutability.
    /// </summary>
    public ProjectStatus Status { get; set; }
    
    public long StatusId { get; set; }

    /// <summary>
    /// Default constructor for the <see cref="Project"/>.
    /// </summary>
    public Project() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class with the specified command.
    /// </summary>
    /// <param name="command">
    /// The command containing the necessary data to create a new project.
    /// </param>
    public Project(CreateProjectCommand command)
    {
        ProjectName = command.ProjectName;
        Description = command.Description;
        DateRange = command.DateRange;
        OrganizationId = command.OrganizationId;
        ContractingEntityId = command.ContractingEntityId;
    }

    /// <summary>
    /// Updates the project name with a new value.
    /// </summary>
    /// <param name="projectName">
    /// The new project name to set, encapsulated in a value object to ensure validation and immutability.
    /// </param>
    public void UpdateProjectName(ProjectName projectName)
    {
        ProjectName = projectName;
    }
    
    /// <summary>
    /// Updates the project description with a new value.
    /// </summary>
    /// <param name="description">
    /// The new project description to set, encapsulated in a value object to ensure validation and immutability.
    /// </param>
    public void UpdateDescription(Description description)
    {
        Description = description;
    }
    
    /// <summary>
    /// Updates the date range of the project with a new value.
    /// </summary>
    /// <param name="status">
    /// The new date range to set, encapsulated in a value object to ensure validation and immutability.
    /// </param>
    public void ReassignStatus(ProjectStatus status)
    {
        Status = status;
    }
}