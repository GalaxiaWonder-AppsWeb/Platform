using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Domain.Model.Entities;

/// <summary>
///     Represents the type of user in the system (e.g., Client, Contractor, Organization).
/// </summary>
public class UserType
{
    /// <summary>
    ///     Gets the unique identifier of the user type.
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    ///     Gets the name of the user type (as an enum value).
    /// </summary>
    public UserTypes Name { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserType"/> class.
    /// </summary>
    public UserType()
    {
    }
    
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserType"/> class with the specified type name.
    /// </summary>
    /// <param name="name">The user type name as an enum value.</param>
    public UserType(UserTypes name)
    {
        Name = name;
    }
}