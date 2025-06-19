using System.ComponentModel.DataAnnotations.Schema;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

/// <summary>
///     Entity representing a person's membership within an organization,
///     including their role or type inside that organization.
/// </summary>
public partial class OrganizationMember
{
    /// <summary>
    ///     Gets the unique identifier of the organization member.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    ///     Gets the ID of the organization to which the person belongs.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; }
    
    /// <summary>
    ///     Gets the ID of the person associated with the membership.
    /// </summary>
    public PersonId PersonId { get; private set; }
    
    /// <summary>
    ///     Gets the type of membership (e.g., CONTRACTOR, WORKER).
    /// </summary>
    public OrganizationMemberType MemberType { get; private set; }
    
    /// <summary>
    ///     Gets the ID of the membership type (used as auxiliary field for persistence).
    /// </summary>
    [NotMapped]
    public long MemberTypeId { get; private set; } //FK Auxiliary

    /// <summary>
    ///     Initializes a new empty instance of <see cref="OrganizationMember"/>. Required by EF Core.
    /// </summary>
    public OrganizationMember() { }

    /// <summary>
    ///     Initializes a new instance of <see cref="OrganizationMember"/> with the specified organization, person, and type.
    /// </summary>
    /// <param name="organizationId">The organization ID.</param>
    /// <param name="personId">The person ID.</param>
    /// <param name="memberType">The type of the member in the organization.</param>
    /// <exception cref="ArgumentNullException">Thrown if organizationId or personId is null.</exception>
    public OrganizationMember(OrganizationId organizationId, PersonId personId, OrganizationMemberType memberType)
    {
        OrganizationId = organizationId ?? throw new ArgumentNullException(nameof(organizationId));
        PersonId = personId ?? throw new ArgumentNullException(nameof(personId));
        MemberType = memberType;
    }
    
    /// <summary>
    ///     Changes the type of the organization member.
    /// </summary>
    /// <param name="newType">The new member type to assign.</param>
    public void ChangeMemberType(OrganizationMemberType newType)
    {
        MemberType = newType;
    }
}