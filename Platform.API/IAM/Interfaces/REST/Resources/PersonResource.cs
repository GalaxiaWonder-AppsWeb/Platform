namespace Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Data transfer object (DTO) representing a person's basic information.
/// </summary>
/// <param name="Id">The unique identifier of the person.</param>
/// <param name="FirstName">The first name of the person.</param>
/// <param name="LastName">The last name of the person.</param>
/// <param name="Email">The email address of the person.</param>
/// <param name="Phone">The phone number of the person.</param>
public record PersonResource(long Id, string FirstName, string LastName, string Email, string Phone);