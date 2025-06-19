namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing the unique identifier of a person.
/// </summary>
public class PersonId
{
    /// <summary>
    ///     Gets the underlying long value of the person identifier.
    /// </summary>
    public long personId { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonId"/> class with a specific value.
    /// </summary>
    /// <param name="value">The long value representing the person ID.</param>
    public PersonId(long value)
    {
        personId = value;
    }

    /// <summary>
    ///     Parameterless constructor required by Entity Framework Core.
    /// </summary>
    private PersonId() { }

    /// <summary>
    ///     Returns the string representation of the person ID.
    /// </summary>
    /// <returns>The person ID as a string.</returns>
    public override string ToString() => personId.ToString();
}

