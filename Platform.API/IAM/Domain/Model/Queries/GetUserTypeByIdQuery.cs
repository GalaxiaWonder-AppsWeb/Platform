namespace Platform.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a user type by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the user type.</param>
public record GetUserTypeByIdQuery(long Id);