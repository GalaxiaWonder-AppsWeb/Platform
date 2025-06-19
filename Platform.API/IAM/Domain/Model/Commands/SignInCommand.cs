namespace Platform.API.IAM.Domain.Model.Commands;

/**
 * <summary>This command is used to sign in a user with the provided username and password.</summary>
 */
public record SignInCommand(string Email, string Password){};