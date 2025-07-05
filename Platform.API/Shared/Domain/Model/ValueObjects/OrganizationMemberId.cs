namespace Platform.API.Shared.Domain.Model.ValueObjects;

public record OrganizationMemberId(long organizationMemberId)
{
    public OrganizationMemberId() : this(0){}
}