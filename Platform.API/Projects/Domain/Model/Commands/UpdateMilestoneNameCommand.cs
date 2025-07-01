using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// Command to update the name of an existing milestone.
/// </summary>
/// <param name="Id">
/// Represents the unique identifier of the milestone to be updated.
/// </param>
/// <param name="MilestoneName">
/// Represents the new name of the milestone.
/// </param>
public record UpdateMilestoneNameCommand(long Id, MilestoneName MilestoneName);