namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to invite a person to an organization using their email address.
/// </summary>
public record InvitePersonToOrganizationByEmailCommand
{
    /// <summary>
    ///     Gets the ID of the organization that sends the invitation.
    /// </summary>
    public long OrganizationId { get;}
    
    /// <summary>
    ///     Gets the email address of the person being invited.
    /// </summary>
    public string Email { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="InvitePersonToOrganizationByEmailCommand"/> class.
    /// </summary>
    /// <param name="organizationId">The unique identifier of the inviting organization.</param>
    /// <param name="email">The email address of the invitee.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="organizationId"/> is zero or 
    ///     <paramref name="email"/> is null, empty, or whitespace.
    /// </exception>
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