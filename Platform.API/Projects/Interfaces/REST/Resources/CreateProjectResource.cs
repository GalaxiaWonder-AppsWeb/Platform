namespace Platform.API.Projects.Interfaces.REST.Resources;

public record CreateProjectResource(string ProjectName, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, long OrganizationId, long ContractingEntityId);