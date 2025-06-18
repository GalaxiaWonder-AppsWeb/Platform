namespace Platform.API.Organizations.Domain.Model.Commands;

public record InvitePersonToOrganizationByEmailCommand
{
    public long OrganizationId { get;}
    public string Email { get; }

    public InvitePersonToOrganizationByEmailCommand(long organizationId, string email)
    {
        if (organizationId == 0L)
            throw new ArgumentException("OrganizationId cannot be zero");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
        OrganizationId = organizationId;
        Email = email;
    }
}