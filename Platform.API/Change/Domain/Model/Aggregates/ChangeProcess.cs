using Platform.API.Change.Domain.Model.Commands;
using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Aggregates;

public partial class ChangeProcess
{
    /// <summary>
    /// Represents the unique identifier for a change process.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Represents the origin of the change process
    /// Wraps the origin of a change process. (ej. TECHNICAL_QUERY, CHANGE_REQUEST)
    /// </summary>
    public ChangeOrigin Origin { get; private set; }
    
    public long OriginId { get; private set; }
    
    /// <summary>
    /// Represents the current status of the change process.
    /// Wraps a status of a change process. (ej. PENDING, APPROVED, REJECTED)
    /// </summary>
    public ChangeProcessStatus Status { get; private set; }
    
    public long StatusId { get; private set; }
    
    /// <summary>
    /// Represents the justification for the change process.
    /// Wraps a content that explains the reason for the change.
    /// </summary>
    public Justification Justification { get; private set; }
    
    /// <summary>
    /// Represents the response to the change process.
    /// Wraps a content that explains the response to the change.
    /// Is optional because the change process can be in a pending state.
    /// </summary>
    public ChangeResponse? Response { get; private set; }
    
    /// <summary>
    /// The unique identifier of the project associated with the change process.
    /// </summary>
    public ProjectId ProjectId { get; private set; }
    
    /// <summary>
    /// Default constructor for the <see cref="ChangeProcess"/> class.
    /// </summary>
    public ChangeProcess(){}

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeProcess"/> class with the specified command.
    /// </summary>
    /// <param name="command">
    /// The command containing the necessary information to create a change process.
    /// </param>
    public ChangeProcess(CreateChangeProcessCommand command)
    {
        Justification = command.Justification;
        ProjectId = command.ProjectId;
    }
    
    /// <summary>
    /// Sets the origin and status of the change process (RELEVANT INFORMATION RELATED TO THE PROJECT STATUS).
    /// </summary>
    /// <param name="origin">
    /// The origin of the change process.
    /// </param>
    /// <param name="status">
    /// The status of the change process.
    /// </param>
    public void SetInformation(ChangeOrigin origin, ChangeProcessStatus status)
    {
        if (Origin is not null && Status is not null)
        {
            throw new InvalidOperationException("Change process information has already been set.");
        }
        Origin = origin;
        Status = status;
    }
    
    /// <summary>
    /// Responds to the change process with the specified response.
    /// </summary>
    /// <param name="response">
    /// Response to the change process.
    /// </param>
    /// <param name="status">
    /// Represents the new status of the change process after the response.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the response has already been set for the change process.
    /// </exception>
    public void RespondToChange(ChangeResponse response, ChangeProcessStatus status)
    {
        if (Response is not null)
        {
            throw new InvalidOperationException("Change process response has already been set.");
        }
        Response = response;
        Status = status;
    }
}