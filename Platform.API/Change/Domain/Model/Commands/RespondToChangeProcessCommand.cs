using Platform.API.Change.Domain.Model.Entities;
using Platform.API.Change.Domain.Model.ValueObjects;

namespace Platform.API.Change.Domain.Model.Commands;

public record RespondToChangeProcessCommand(long Id, ChangeResponse Response, ChangeProcessStatus Status);