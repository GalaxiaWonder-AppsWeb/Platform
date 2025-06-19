namespace Platform.API.IAM.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a person's details by their unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the person.</param>
public record GetPersonByIdQuery(long Id);