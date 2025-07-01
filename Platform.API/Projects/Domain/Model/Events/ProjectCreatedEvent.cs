using Platform.API.Shared.Domain.Model.Events;

namespace Platform.API.Projects.Domain.Model.Events;

public class ProjectCreatedDomainEvent : DomainEvent
{
    public long ProjectId { get; }
    public long OrganizationId { get; }

    public ProjectCreatedDomainEvent(long projectId, long organizationId)
    {
        ProjectId = projectId;
        OrganizationId = organizationId;
    }
}
