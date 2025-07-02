namespace Platform.API.Projects.Domain.Model.Queries;

public record GetAllTasksByPersonIdAndMilestoneIdQuery(long PersonId, long MilestoneId);