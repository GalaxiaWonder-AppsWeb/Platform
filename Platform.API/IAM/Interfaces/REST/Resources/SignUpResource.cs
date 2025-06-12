namespace Platform.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password, string UserType, string FirstName, string LastName, string Email, string? Phone);