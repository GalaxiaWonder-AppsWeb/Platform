using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

public record UpdateProjectNameCommand(ProjectName ProjectName);