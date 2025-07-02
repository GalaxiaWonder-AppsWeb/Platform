namespace Platform.API.Projects.Interfaces.REST.Resources;

public record CreateTaskResource(string Name, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, long MilestoneId, string Specialty, string? Status, long? PersonId);