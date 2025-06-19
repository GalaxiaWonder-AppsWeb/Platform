namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
/// Command to reject an existing organization invitation.
/// </summary>
public record RejectInvitationCommand
{
    /// <summary>
    /// Gets the ID of the invitation to be rejected.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RejectInvitationCommand"/> class.
    /// </summary>
    /// <param name="id">The ID of the invitation.</param>
    /// <exception cref="ArgumentException">Thrown when the ID is 0.</exception>
    public RejectInvitationCommand(long id)
    {
        if(id == 0L)
            throw new ArgumentException("Id cannot be zero");
        Id = id;
    }
}