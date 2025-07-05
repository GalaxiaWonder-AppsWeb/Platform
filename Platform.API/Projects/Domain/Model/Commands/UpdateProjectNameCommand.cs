using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

public record UpdateProjectNameCommand(long Id, ProjectName ProjectName);