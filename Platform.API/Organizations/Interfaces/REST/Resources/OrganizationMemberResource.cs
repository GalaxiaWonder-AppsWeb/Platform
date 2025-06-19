namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a member of an organization, including personal and role-related information.
/// </summary>
/// <param name="Id">The unique identifier of the organization member.</param>
/// <param name="FirstName">The first name of the member.</param>
/// <param name="LastName">The last name of the member.</param>
/// <param name="Email">The email address of the member.</param>
/// <param name="MemberType">The type or role of the member within the organization (e.g., CONTRACTOR, WORKER).</param>
/// <param name="JoinedAt">The date and time when the member joined the organization.</param>
public record OrganizationMemberResource(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string MemberType,
    DateTimeOffset JoinedAt
);