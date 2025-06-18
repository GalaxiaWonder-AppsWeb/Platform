namespace Platform.API.Organizations.Domain.Model.Commands;

public record RejectInvitationCommand
{
    public long Id { get; }

    public RejectInvitationCommand(long id)
    {
        if(id == 0L)
            throw new ArgumentException("Id cannot be zero");
        Id = id;
    }
}