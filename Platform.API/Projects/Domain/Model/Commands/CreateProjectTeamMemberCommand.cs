using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Commands;

public record CreateProjectTeamMemberCommand(ProjectId ProjectId, Specialty Specialty, Role Role, OrganizationMemberId OrganizationMemberId);