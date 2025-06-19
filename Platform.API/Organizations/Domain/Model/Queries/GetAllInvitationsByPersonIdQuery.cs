namespace Platform.API.Organizations.Domain.Model.Queries;

/// <summary>
/// Query to retrieve all invitations received by a specific person.
/// </summary>
/// <param name="PersonId">The unique identifier of the person.</param>
public record GetAllInvitationsByPersonIdQuery(long PersonId);