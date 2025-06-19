using System.ComponentModel.DataAnnotations.Schema;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root representing an organization entity in the domain.
///     Contains identity, legal and commercial details, status, and references to members and invitations.
/// </summary>
public partial class Organization
{
    /// <summary>
    ///     Gets the unique identifier of the organization.
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    ///     Gets the legal name of the organization.
    /// </summary>
    public LegalName LegalName { get; private set; }
    
    /// <summary>
    ///     Gets the commercial name of the organization.
    /// </summary>
    public CommercialName CommercialName { get; private set; }
    
    /// <summary>
    ///     Gets the RUC (Registro Único de Contribuyentes) of the organization.
    /// </summary>
    public RUC Ruc { get; private set; }

    /// <summary>
    ///     Gets the identifier of the person who created the organization.
    /// </summary>
    public PersonId CreatedBy { get; private set; }

    /// <summary>
    ///     Gets the ID of the organization status (used as auxiliary field for persistence).
    /// </summary>
    [NotMapped]
    public long OrganizationStatusId { get; private set; }
    
    /// <summary>
    ///     Gets the current status of the organization.
    /// </summary>
    public OrganizationStatus Status { get; private set; }


    private readonly List<long> _organizationMemberIds = new(); 
    private readonly List<long> _organizationInvitationIds = new();

    /// <summary>
    ///     Initializes a new empty instance of <see cref="Organization"/>. Required by EF Core.
    /// </summary>
    public Organization()
    {
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="Organization"/> with full details including status.
    /// </summary>
    public Organization(LegalName legalName, CommercialName commercialName, RUC ruc, PersonId createdBy, OrganizationStatus status)
    {
        LegalName = legalName;
        CommercialName = commercialName;
        Ruc = ruc;
        CreatedBy = createdBy;
        Status = status;
    }
    
    /// <summary>
    ///     Initializes a new instance of <see cref="Organization"/> without status (to be assigned later).
    /// </summary>
    public Organization(LegalName legalName , CommercialName commercialName, RUC ruc, PersonId createdBy)
    {
        LegalName = legalName;
        CommercialName = commercialName;
        Ruc = ruc;
        CreatedBy = createdBy;
    }
    
    /// <summary>
    ///     Initializes a new instance of <see cref="Organization"/> with default commercial name and status.
    /// </summary>
    public Organization(LegalName legalName , RUC ruc, PersonId createdBy, OrganizationStatus status)
    {
        LegalName = legalName;
        CommercialName = new CommercialName("");
        Ruc = ruc;
        CreatedBy = createdBy;
        Status = status;
    }

    /// <summary>
    ///     Updates the legal name of the organization.
    /// </summary>
    public void EditLegalName(LegalName legalName)
    {
        LegalName = legalName;
    }

    /// <summary>
    ///     Updates the commercial name of the organization.
    /// </summary>
    public void EditCommercialName(CommercialName commercialName)
    {
        CommercialName = commercialName;
    }

    /// <summary>
    ///     Assigns a new status to the organization.
    /// </summary>
    public void AssignStatus(OrganizationStatus status)
    {
        Status = status;
    }

    public IReadOnlyList<long> OrganizationMemberIds => _organizationMemberIds.AsReadOnly();
    public IReadOnlyList<long> OrganizationInvitationIds => _organizationInvitationIds.AsReadOnly();

    public void AddOrganizationMember(long memberId)
    {
        if (!_organizationMemberIds.Contains(memberId))
            _organizationMemberIds.Add(memberId);
    }

    public void AddOrganizationInvitation(long invitationId)
    {
        if (!_organizationInvitationIds.Contains(invitationId))
            _organizationInvitationIds.Add(invitationId);
    }
    
    public void SetMemberIds(IEnumerable<long> ids)
    {
        _organizationMemberIds.Clear();
        _organizationMemberIds.AddRange(ids);
    }

    public void SetInvitationIds(IEnumerable<long> ids)
    {
        _organizationInvitationIds.Clear();
        _organizationInvitationIds.AddRange(ids);
    }
}