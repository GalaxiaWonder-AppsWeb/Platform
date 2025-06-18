namespace Platform.API.Organizations.Domain.Model.Commands;

public record AcceptInvitationCommand
{
    public long Id { get; }

    public AcceptInvitationCommand(long id)
    {
        if(id == 0L)
            throw new ArgumentException("Id cannot be zero");
        Id = id;
    }
}