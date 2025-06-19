namespace Platform.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to check if a user account exists with the given email address.
/// </summary>
/// <param name="Email">The email address to check for existence.</param>
public record ExistsByEmailQuery(string Email)
{
    
};