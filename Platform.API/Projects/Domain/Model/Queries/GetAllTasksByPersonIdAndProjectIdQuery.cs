namespace Platform.API.Projects.Domain.Model.Queries;

public record GetAllTasksByPersonIdAndProjectIdQuery(long PersonId, long ProjectId);