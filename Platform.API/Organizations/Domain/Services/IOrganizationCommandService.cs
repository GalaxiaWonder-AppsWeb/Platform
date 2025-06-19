using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Services;

public interface IOrganizationCommandService
{
    Task<Organization> Handle(CreateOrganizationCommand command);
    Task Handle(DeleteOrganizationCommand command);
    Task<Organization> Handle(UpdateOrganizationCommand command);
    Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(InvitePersonToOrganizationByEmailCommand command);
    Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(AcceptInvitationCommand command);
    Task<(Organization, OrganizationInvitation, ProfileDetails)> Handle(RejectInvitationCommand command);
    Task Handle(DeleteOrganizationMemberCommand command);
}