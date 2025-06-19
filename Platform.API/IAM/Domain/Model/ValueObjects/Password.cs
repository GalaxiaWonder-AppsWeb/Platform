namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a hashed password.
/// </summary>
/// <param name="HashedPassword">The hashed password string.</param>
public record Password(string HashedPassword)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Password"/> record with an empty hashed password.
    /// </summary>
    public Password() : this(string.Empty){}
}