namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a person's full name.
/// </summary>
/// <param name="FirstName">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
public record PersonName(string FirstName, string LastName)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonName"/> record with empty first and last names.
    /// </summary>
    public PersonName() : this(string.Empty, string.Empty)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonName"/> record with a first name only.
    /// </summary>
    /// <param name="firstName">The first name of the person.</param>
    public PersonName(string firstName) : this(firstName, string.Empty)
    {
    }
    
    /// <summary>
    ///     Gets the full name, combining first and last names.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
};