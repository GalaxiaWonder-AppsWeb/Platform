namespace Platform.API.Organizations.Interfaces.REST.Resources;

public record OrganizationMemberResource(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string MemberType,
    DateTimeOffset JoinedAt
);