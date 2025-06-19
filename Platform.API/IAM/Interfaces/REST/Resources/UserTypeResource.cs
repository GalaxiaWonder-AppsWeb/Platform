namespace Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Resource representing a user type in the system.
/// </summary>
/// <param name="Id">The unique identifier of the user type.</param>
/// <param name="Name">The name of the user type (e.g., CLIENT, WORKER).</param>
public record UserTypeResource(long Id, string Name);