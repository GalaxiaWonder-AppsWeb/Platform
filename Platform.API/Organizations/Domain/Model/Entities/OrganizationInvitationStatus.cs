using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

public class OrganizationInvitationStatus
{
    public long Id { get; private set; }
    
    public OrganizationInvitationStatuses Name { get; private set; }
    
    public OrganizationInvitationStatus(){}

    public OrganizationInvitationStatus(OrganizationInvitationStatuses name)
    {
        Name = name;
    }
}