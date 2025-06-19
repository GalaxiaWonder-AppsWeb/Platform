namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a username used for authentication.
/// </summary>
/// <param name="Username">The string value of the username.</param>
public record UserName(string Username)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserName"/> record with an empty username.
    /// </summary>
    public UserName() : this(string.Empty){}
}