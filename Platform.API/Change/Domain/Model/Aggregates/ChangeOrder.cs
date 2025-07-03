using Platform.API.Change.Domain.Model.Commands;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Aggregates;

public partial class ChangeOrder
{
    /// <summary>
    /// Represents the unique identifier for a change order.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// The unique identifier for the milestone associated with this change order.
    /// </summary>
    public MilestoneId MilestoneId { get; private set; }
    
    /// <summary>
    /// Represents the description of the change order.
    /// Wraps the content justification for the change.
    /// </summary>
    public Justification Description { get; private set; }
    
    /// <summary>
    /// The unique identifier for the change process this change order belongs to.
    /// </summary>
    public ChangeProcessId ChangeProcessId { get; private set; }
    
    /// <summary>
    /// Default constructor for the <see cref="ChangeOrder"/> class.
    /// </summary>
    public ChangeOrder(){}

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeOrder"/> class with the specified command.
    /// </summary>
    /// <param name="command">
    /// Command containing the necessary data to create a change order.
    /// </param>
    public ChangeOrder(CreateChangeOrderCommand command)
    {
        MilestoneId = command.MilestoneId;
        Description = command.Description;
        ChangeProcessId = command.ChangeProcessId;
    }
}