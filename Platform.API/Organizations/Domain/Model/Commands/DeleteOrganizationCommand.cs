namespace Platform.API.Organizations.Domain.Model.Commands;

public record DeleteOrganizationCommand
{
    public long Id { get; }

    public DeleteOrganizationCommand(long id)
    {
        if(id == 0L)
            throw new ArgumentException("Organization id cannot be 0!");
        
        Id = id;
    }
}
