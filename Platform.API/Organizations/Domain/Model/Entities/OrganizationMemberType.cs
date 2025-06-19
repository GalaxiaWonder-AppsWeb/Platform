using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

/// <summary>
///     Value object representing the type or role of a member within an organization,
///     such as CONTRACTOR or WORKER.
/// </summary>
public class OrganizationMemberType
{
    /// <summary>
    ///     Gets the unique identifier of the organization member type.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    ///     Gets the name of the member type as an enum value.
    /// </summary>
    public OrganizationMemberTypes Name { get; private set; }
    
    /// <summary>
    ///     Initializes a new empty instance of <see cref="OrganizationMemberType"/>.
    ///     Required by EF Core.
    /// </summary>
    public OrganizationMemberType(){}
    
    /// <summary>
    ///     Initializes a new instance of <see cref="OrganizationMemberType"/> with the specified enum value.
    /// </summary>
    /// <param name="name">The enum value representing the member type.</param>
    public OrganizationMemberType(OrganizationMemberTypes name)
    {
        Name = name;
    }
}