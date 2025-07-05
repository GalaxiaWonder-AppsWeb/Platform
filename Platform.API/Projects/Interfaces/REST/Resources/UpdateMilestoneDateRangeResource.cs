namespace Platform.API.Projects.Interfaces.REST.Resources;

public record UpdateMilestoneDateRangeResource(DateTimeOffset StartDate, DateTimeOffset EndDate);