using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

public class OrganizationMemberType
{
    public long Id { get; private set; }
    
    public OrganizationMemberTypes Name { get; private set; }
    
    public OrganizationMemberType(){}

    public OrganizationMemberType(OrganizationMemberTypes name)
    {
        Name = name;
    }
}