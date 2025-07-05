
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Commands;

public record CreateChangeOrderCommand(MilestoneId MilestoneId, Justification Description, ChangeProcessId ChangeProcessId);