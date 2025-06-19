using System.Text.Json.Serialization;
using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Data transfer object (DTO) representing the data required to register a new user account.
/// </summary>
/// <param name="Username">The desired username for the new account.</param>
/// <param name="Password">The plaintext password to be hashed and stored.</param>
/// <param name="UserType">The type of user (e.g., CLIENT, WORKER), serialized as a string in JSON.</param>
/// <param name="FirstName">The first name of the person associated with the account.</param>
/// <param name="LastName">The last name of the person associated with the account.</param>
/// <param name="Email">The email address of the person.</param>
/// <param name="Phone">The phone number of the person (optional).</param>
public record SignUpResource(
    string Username,
    string Password,

    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    UserTypes UserType,

    string FirstName,
    string LastName,
    string Email,
    string? Phone);