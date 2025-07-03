using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Domain.Model.Commands;

namespace Platform.API.Change.Domain.Services;

public interface IChangeProcessCommandService
{
    /// <summary>
    /// Handles the creation of a new change process.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create the change process, including the organization ID, project ID, and change request details.
    /// </param>
    /// <returns>
    /// The newly created <see cref="ChangeProcess"/> entity, or null if the creation failed.
    /// </returns>
    Task<ChangeProcess?> Handle(CreateChangeProcessCommand command);
    
    /// <summary>
    /// Handles the responding to a change process.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to respond to the change process, including the change process ID and response details.
    /// </param>
    /// <returns>
    /// The updated <see cref="ChangeProcess"/> entity, or null if the response handling failed.
    /// </returns>
    Task<ChangeProcess?> Handle(RespondToChangeProcessCommand command);
}