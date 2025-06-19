namespace Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Data transfer object (DTO) representing the information required for a user to sign in.
/// </summary>
/// <param name="Email">The email address used to authenticate the user.</param>
/// <param name="Password">The plaintext password entered by the user.</param>
public record SignInResource(string Email, string Password);