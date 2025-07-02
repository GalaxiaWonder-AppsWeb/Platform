using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Interfaces.REST.Resources;

public record ProjectResource(long Id, string ProjectName, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, decimal Budget, long OrganizationId, long ContractingEntityId, long Contractor, string Status);