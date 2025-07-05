namespace Platform.API.Projects.Interfaces.REST.Resources;

public record UpdateProjectDateRangeResource(DateTimeOffset StartDate, DateTimeOffset EndDate);