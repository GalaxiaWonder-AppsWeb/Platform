using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to create a new member within an organization.
/// </summary>
public record CreateOrganizationMemberCommand
{
    /// <summary>
    ///     Gets the ID of the person to be added as a member.
    /// </summary>
    public long PersonId { get; }
    
    /// <summary>
    ///     Gets the ID of the organization to which the person will be added.
    /// </summary>
    public long OrganizationId { get; }
    
    /// <summary>
    ///     Gets the type of membership assigned to the person (e.g., CONTRACTOR, WORKER).
    /// </summary>
    public OrganizationMemberTypes MemberType { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateOrganizationMemberCommand"/> class.
    /// </summary>
    /// <param name="personId">The ID of the person to be added.</param>
    /// <param name="organizationId">The ID of the target organization.</param>
    /// <param name="memberType">The type of member being assigned.</param>
    /// <exception cref="ArgumentException">Thrown when any required argument is invalid (zero or default).</exception>
    public CreateOrganizationMemberCommand(long personId, long organizationId,
        OrganizationMemberTypes memberType)
    {
        if (personId == 0L) 
            throw new ArgumentException("Person id cannot be 0");
        if (organizationId == 0L)
            throw new ArgumentException("Organization id cannot be 0");
        if (memberType == default(OrganizationMemberTypes))
            throw new ArgumentException("Member type cannot be empty");
        PersonId = personId;
        OrganizationId = organizationId;
        MemberType = memberType;
    }
}