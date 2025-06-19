namespace Platform.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to register a new user account in the system.
/// </summary>
/// <param name="Username">The username for the new account.</param>
/// <param name="Password">The password for the new account (plaintext or hashed depending on handling).</param>
/// <param name="UserType">The type of user (e.g., CLIENT, CONTRACTOR, ORGANIZATION).</param>
/// <param name="FirstName">The first name of the person associated with the account.</param>
/// <param name="LastName">The last name of the person associated with the account.</param>
/// <param name="Email">The email address of the person.</param>
/// <param name="Phone">The phone number of the person (optional).</param>
public record SignUpCommand(string Username, string Password, string UserType, string FirstName, string LastName, string Email, string? Phone){};