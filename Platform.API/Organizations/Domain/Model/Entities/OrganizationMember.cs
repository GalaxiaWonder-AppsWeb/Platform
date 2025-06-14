using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

public partial class OrganizationMember
{
    public long Id { get; private set; }
    
    public OrganizationId OrganizationId { get; private set; }
    public PersonId PersonId { get; private set; }
    
    public OrganizationMemberType MemberType { get; private set; }

    public OrganizationMember() { }

    public OrganizationMember(OrganizationId organizationId, PersonId personId, OrganizationMemberType memberType)
    {
        OrganizationId = organizationId ?? throw new ArgumentNullException(nameof(organizationId));
        PersonId = personId ?? throw new ArgumentNullException(nameof(personId));
        MemberType = memberType;
    }

    public void ChangeMemberType(OrganizationMemberType newType)
    {
        MemberType = newType;
    }
}