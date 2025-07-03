using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Commands;

public record CreateChangeProcessCommand(Justification Justification, ProjectId ProjectId);