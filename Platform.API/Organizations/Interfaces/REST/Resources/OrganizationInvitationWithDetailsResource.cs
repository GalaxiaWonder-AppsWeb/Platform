namespace Platform.API.Organizations.Interfaces.REST.Resources;

public class OrganizationInvitationWithDetailsResource
{
    public long Id { get; set; }
    public string OrganizationName { get; set; } = string.Empty;
    public string InvitedByFullName { get; set; } = string.Empty;
    public string InvitedByEmail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
