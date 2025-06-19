namespace Platform.API.Organizations.Interfaces.REST.Resources;

public record ProfileDetailsResource(
    long Id,
    string Name,
    string LastName,
    string Email
);