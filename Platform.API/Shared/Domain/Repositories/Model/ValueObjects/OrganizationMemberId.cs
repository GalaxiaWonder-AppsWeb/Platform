namespace Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

public record OrganizationMemberId(long organizationMemberId)
{
    public OrganizationMemberId() : this(0){}
}