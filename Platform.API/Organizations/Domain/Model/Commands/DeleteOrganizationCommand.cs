namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to delete an existing organization by its unique identifier.
/// </summary>
public record DeleteOrganizationCommand
{
    /// <summary>
    ///     Gets the ID of the organization to be deleted.
    /// </summary>
    public long Id { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DeleteOrganizationCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the organization to delete.</param>
    /// <exception cref="ArgumentException">Thrown if the provided <paramref name="id"/> is zero.</exception>
    public DeleteOrganizationCommand(long id)
    {
        if(id == 0L)
            throw new ArgumentException("Organization id cannot be 0!");
        
        Id = id;
    }
}
