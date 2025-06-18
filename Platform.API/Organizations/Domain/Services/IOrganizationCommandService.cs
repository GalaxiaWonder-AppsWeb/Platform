using Platform.API.Organizations.Domain.Model.Aggregates;
using Platform.API.Organizations.Domain.Model.Commands;

namespace Platform.API.Organizations.Domain.Services;

public interface IOrganizationCommandService
{
    Task<Organization> Handle(CreateOrganizationCommand command);
    Task Handle(DeleteOrganizationCommand command);
    Task<Organization> Handle(UpdateOrganizationCommand command);
}