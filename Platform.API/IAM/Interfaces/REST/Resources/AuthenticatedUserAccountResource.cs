namespace Platform.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserAccountResource(long Id, string Username, string UserType, string Token);