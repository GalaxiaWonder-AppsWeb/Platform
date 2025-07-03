using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;
using Task = System.Threading.Tasks.Task;

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
    
    /// <summary>
    /// Handles the update of the milestone name.
    /// </summary>
    /// <param name="command">
    /// The command containing the milestone ID and the new name for the milestone.
    /// </param>
    /// <returns>
    /// The updated <see cref="Milestone"/> entity, or null if the update failed.
    /// </returns>
    Task<Milestone?> Handle(UpdateMilestoneNameCommand command);
    
    /// <summary>
    /// Handles the update of the milestone description.
    /// </summary>
    /// <param name="command">
    /// The command containing the milestone ID and the new description for the milestone.
    /// </param>
    /// <returns>
    /// The updated <see cref="Milestone"/> entity, or null if the update failed.
    /// </returns>
    Task<Milestone?> Handle(UpdateMilestoneDescriptionCommand command);
    
    /// <summary>
    /// Handles the update of the milestone date range.
    /// </summary>
    /// <param name="command">
    /// The command containing the milestone ID and the new start and end dates for the milestone.
    /// </param>
    /// <returns>
    /// The updated <see cref="Milestone"/> entity, or null if the update failed.
    /// </returns>
    Task<Milestone?> Handle(UpdateMilestoneDateRangeCommand command);
    
    /// <summary>
    /// Handles the deletion of an existing milestone.
    /// </summary>
    /// <param name="command">
    /// The command specifying the milestone to delete, including the milestone ID.
    /// </param>
    Task Handle (DeleteMilestoneCommand command);
}