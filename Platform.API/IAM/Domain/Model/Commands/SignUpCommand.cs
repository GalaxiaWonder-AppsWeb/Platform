namespace Platform.API.IAM.Domain.Model.Commands;

public record SignUpCommand(string Username, string Password, string UserType, string FirstName, string LastName, string Email, string? Phone){};