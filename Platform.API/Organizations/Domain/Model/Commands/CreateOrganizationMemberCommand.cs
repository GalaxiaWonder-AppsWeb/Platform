using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Commands;

public record CreateOrganizationMemberCommand
{
    public long PersonId { get; }
    public long OrganizationId { get; }
    public OrganizationMemberTypes MemberType { get; }

    public CreateOrganizationMemberCommand(long personId, long organizationId,
        OrganizationMemberTypes memberType)
    {
        if (personId == 0L) 
            throw new ArgumentException("Person id cannot be 0");
        if (organizationId == 0L)
            throw new ArgumentException("Organization id cannot be 0");
        if (memberType == default(OrganizationMemberTypes))
            throw new ArgumentException("Member type cannot be empty");
        PersonId = personId;
        OrganizationId = organizationId;
        MemberType = memberType;
    }
}