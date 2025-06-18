namespace Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

public class OrganizationId
{
    public long organizationId { get; private set; }

    public OrganizationId(long value)
    {
        organizationId = value;
    }

    public OrganizationId() : this(0L) { }

    public override string ToString() => organizationId.ToString();
}