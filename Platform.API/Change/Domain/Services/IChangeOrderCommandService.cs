using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Domain.Model.Commands;

namespace Platform.API.Change.Domain.Services;

public interface IChangeOrderCommandService
{
    /// <summary>
    /// Handles the creation of a new change order.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create the change order, including the organization ID, project ID, and change request details.
    /// </param>
    /// <returns>
    /// The newly created <see cref="ChangeOrder"/> entity, or null if the creation failed.
    /// </returns>
    Task<ChangeOrder?> Handle(CreateChangeOrderCommand command);
}