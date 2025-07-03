using Platform.API.Change.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Entities;

/// <summary>
/// Entity that represents a change process status in the change management domain.
/// This class is backed by the <see cref="ChangeProcessStatuses"/> enum and defines
/// a fixed set of statuses used to categorize the state of change processes.
/// </summary>
public class ChangeProcessStatus
{
    /// <summary>
    /// Database identifier for the change process status entity.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Enum description representing the status of the change process.
    /// </summary>
    public ChangeProcessStatuses Name { get; set; }
    
    /// <summary>
    /// Default constructor for the <see cref="ChangeProcessStatus"/> class.
    /// </summary>
    public ChangeProcessStatus() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeProcessStatus"/> class with the specified status name.
    /// </summary>
    /// <param name="name">
    /// The specific status name of the change process, represented by the <see cref="ChangeProcessStatuses"/> enum.
    /// </param>
    public ChangeProcessStatus(ChangeProcessStatuses name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Get the name of the change process status.
    /// </summary>
    /// <returns>
    /// The <see cref="ChangeProcessStatuses"/> enum value representing the status name.
    /// </returns>
    public ChangeProcessStatuses GetName()
    {
        return Name;   
    }
}