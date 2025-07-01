using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Domain.Services;

public interface IMilestoneCommandService
{
    /// <summary>
    /// Handles the creation of a new milestone.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create the milestone, including the project ID, name, and description.
    /// </param>
    /// <returns>
    /// The newly created <see cref="Milestone"/> entity, or null if the creation failed.
    /// </returns>
    Task<Milestone?> Handle(CreateMilestoneCommand command);
}