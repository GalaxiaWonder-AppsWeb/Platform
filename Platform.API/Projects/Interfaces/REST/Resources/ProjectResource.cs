namespace Platform.API.Projects.Interfaces.REST.Resources;

public record ProjectResource(long Id, string ProjectName, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, long OrganizationId, long ContractingEntityId);