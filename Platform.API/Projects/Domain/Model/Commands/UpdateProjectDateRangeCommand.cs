using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// Command to update the date range of a project.
/// </summary>
/// <param name="Id">
/// The unique identifier of the project.
/// </param>
/// <param name="DateRange">
/// The new date range for the project, wraps a start and end date.
/// </param>
public record UpdateProjectDateRangeCommand(long Id, DateRange DateRange);