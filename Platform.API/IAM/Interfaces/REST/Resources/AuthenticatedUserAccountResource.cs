namespace Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing an authenticated user account,
///     including identity, role, and access token information.
/// </summary>
/// <param name="Id">The unique identifier of the user account.</param>
/// <param name="Username">The username used for authentication.</param>
/// <param name="UserType">The type of user (e.g., CLIENT, WORKER).</param>
/// <param name="Token">The JWT token assigned to the authenticated session.</param>
/// <param name="PersonId">The unique identifier of the associated person entity.</param>
public record AuthenticatedUserAccountResource(long Id, string Username, string UserType, string Token, long PersonId);