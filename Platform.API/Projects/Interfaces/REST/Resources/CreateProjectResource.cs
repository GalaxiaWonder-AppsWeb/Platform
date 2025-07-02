using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Interfaces.REST.Resources;

public record CreateProjectResource(string ProjectName, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, decimal Budget, long OrganizationId, string ContractingEntityEmail);