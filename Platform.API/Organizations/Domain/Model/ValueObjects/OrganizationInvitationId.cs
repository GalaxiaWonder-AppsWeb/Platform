namespace Platform.API.Organizations.Domain.Model.ValueObjects;

public record OrganizationInvitationId(long organizationInvitationId)
{
    public OrganizationInvitationId() : this(0){}
}