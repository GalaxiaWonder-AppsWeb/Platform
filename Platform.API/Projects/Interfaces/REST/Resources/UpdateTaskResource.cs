namespace Platform.API.Projects.Interfaces.REST.Resources;

public record UpdateTaskResource(string? Name, string? Description, string? Status, DateTimeOffset? StartDate, DateTimeOffset? EndDate, long? PersonId, bool RemovePerson = false);