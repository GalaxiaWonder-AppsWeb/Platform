using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Entities;

/// <summary>
/// Entity that represents a specialty in the project domain.
/// this class is backed by the <see cref="Specialties"/> enum and defines
/// a fixed set of specialties used to categorize project team members and tasks.
/// </summary>
public class Specialty
{
    /// <summary>
    /// Database identifier for the specialty entity.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Enum description representing the specialty of the project team members or tasks.
    /// </summary>
    public Specialties Name { get; set; }
    
    /// <summary>
    /// Initializes a new empty instance of <see cref="Specialty"/>.
    /// </summary>
    public Specialty() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="Specialty"/> with the specified specialty name.
    /// </summary>
    /// <param name="name">
    /// The specialty name of the project, represented by the <see cref="Specialties"/> enum.
    /// </param>
    public Specialty(Specialties name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Get the name of the specialty.
    /// </summary>
    /// <returns>
    /// The <see cref="Specialties"/> enum value representing the specialty name.
    /// </returns>
    public Specialties GetName()
    {
        return Name;
    }
}