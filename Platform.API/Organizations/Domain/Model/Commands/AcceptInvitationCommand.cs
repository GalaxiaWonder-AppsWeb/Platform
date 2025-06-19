namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to accept an organization invitation.
/// </summary>
public record AcceptInvitationCommand
{
    /// <summary>
    ///     Gets the unique identifier of the invitation to accept.
    /// </summary>
    public long Id { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AcceptInvitationCommand"/> class
    ///     with the specified invitation ID.
    /// </summary>
    /// <param name="id">The ID of the invitation to accept.</param>
    /// <exception cref="ArgumentException">Thrown when the ID is zero.</exception>
    public AcceptInvitationCommand(long id)
    {
        if(id == 0L)
            throw new ArgumentException("Id cannot be zero");
        Id = id;
    }
}