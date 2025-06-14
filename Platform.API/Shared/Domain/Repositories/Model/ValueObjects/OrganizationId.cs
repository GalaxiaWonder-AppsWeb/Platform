namespace Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

public record OrganizationId(long organizationId)
{
    public OrganizationId() : this(0){}
}