namespace Platform.API.Projects.Interfaces.REST.Resources;

public record MilestoneResource(long Id, string Name, string Description, long ProjectId, DateTimeOffset StartDate, DateTimeOffset EndDate);