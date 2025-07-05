using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// Command to update the date range of an existing milestone.
/// </summary>
/// <param name="Id">
/// The unique identifier of the milestone to be updated.
/// </param>
/// <param name="DateRange">
/// The date range for the milestone, indicating the start and end dates.
/// </param>
public record UpdateMilestoneDateRangeCommand(long Id, DateRange DateRange);