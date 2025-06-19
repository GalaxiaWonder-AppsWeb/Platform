namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Represents detailed profile information of a person within the system.
/// </summary>
/// <param name="Id">The unique identifier of the person.</param>
/// <param name="Name">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="Email">The email address associated with the person.</param>
public record ProfileDetailsResource(
    long Id,
    string Name,
    string LastName,
    string Email
);