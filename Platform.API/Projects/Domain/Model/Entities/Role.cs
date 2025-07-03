using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Entities;

/// <summary>
/// Entity that represents a role in the project domain.
/// This class is backed by the <see cref="Roles"/> enum and defines
/// a fixed set of roles used to categorize project team members.
/// </summary>
public class Role
{
    /// <summary>
    /// Database identifier for the role entity.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Enum description representing the role of the project team members.
    /// </summary>
    public Roles Name { get; set; }
    
    /// <summary>
    /// Initializes a new empty instance of <see cref="Role"/>.
    /// </summary>
    public Role() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="Role"/> with the specified role name.
    /// </summary>
    /// <param name="name">
    /// The specific role name of the project, represented by the <see cref="Roles"/> enum.
    /// </param>
    public Role(Roles name)
    {
        Name = name;
    }

    /// <summary>
    /// Get the name of the role.
    /// </summary>
    /// <returns>
    /// The <see cref="Roles"/> enum value representing the role name.
    /// </returns>
    public Roles GetName()
    {
        return Name;   
    }
}