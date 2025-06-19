namespace Platform.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a user account by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the user account.</param>
public record GetUserAccountByIdQuery(long Id);