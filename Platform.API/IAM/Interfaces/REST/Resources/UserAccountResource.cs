namespace Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing a user account for output purposes,
///     including identity and user type information.
/// </summary>
/// <param name="Id">The unique identifier of the user account.</param>
/// <param name="Username">The username associated with the account.</param>
/// <param name="UserType">The type of the user (e.g., CLIENT, WORKER).</param>
/// <param name="PersonId">The identifier of the person linked to this account (as a string).</param>
public record UserAccountResource(long Id, string Username, string UserType, string PersonId);