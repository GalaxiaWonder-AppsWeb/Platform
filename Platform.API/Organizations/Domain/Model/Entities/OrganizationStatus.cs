using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

public class OrganizationStatus
{
    public long Id { get; private set; }
    
    public OrganizationStatuses Name { get; private set; }
    
    public OrganizationStatus(){}
    
    public OrganizationStatus(OrganizationStatuses name)
    {
        Name = name;
    }
}