using Platform.API.Change.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Entities;

public class ChangeOrigin
{
    /// <summary>
    /// Database identifier for the change process status entity.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Enum description representing the origin of the change.
    /// </summary>
    public ChangeOrigins Name { get; set; }
    
    /// <summary>
    /// Default constructor for the <see cref="ChangeOrigin"/> class.
    /// </summary>
    public ChangeOrigin() { }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeOrigin"/> class with the specified origin name.
    /// </summary>
    /// <param name="name">
    /// The specific origin name of the change, represented by the <see cref="ChangeOrigins"/> enum.
    /// </param>
    public ChangeOrigin(ChangeOrigins name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Get the name of the change origin.
    /// </summary>
    /// <returns>
    /// The <see cref="ChangeOrigins"/> enum value representing the origin name.
    /// </returns>
    public ChangeOrigins GetName()
    {
        return Name;   
    }
}