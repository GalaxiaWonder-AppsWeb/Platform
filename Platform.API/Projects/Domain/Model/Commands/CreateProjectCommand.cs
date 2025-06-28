using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

public record CreateProjectCommand(ProjectName ProjectName, Description Description, DateRange DateRange, OrganizationId OrganizationId, PersonId ContractingEntityId, ProjectStatus Status);