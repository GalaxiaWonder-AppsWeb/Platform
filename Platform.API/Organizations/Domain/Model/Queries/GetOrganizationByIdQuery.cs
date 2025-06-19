namespace Platform.API.Organizations.Domain.Model.Queries;

/// <summary>
/// Query to retrieve a specific organization by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the organization.</param>
public record GetOrganizationByIdQuery(long Id);