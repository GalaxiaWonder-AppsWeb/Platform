using System.ComponentModel.DataAnnotations.Schema;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Aggregates;

public partial class Organization
{
    public long Id { get; private set; }

    public LegalName LegalName { get; private set; }
    public CommercialName CommercialName { get; private set; }
    public RUC Ruc { get; private set; }

    public PersonId CreatedBy { get; private set; }

    [NotMapped]
    public long OrganizationStatusId { get; private set; } // auxiliary
    public OrganizationStatus Status { get; private set; }


    private readonly List<long> _organizationMemberIds = new();
    private readonly List<long> _organizationInvitationIds = new();

    public Organization()
    {
    }

    public Organization(LegalName legalName, CommercialName commercialName, RUC ruc, PersonId createdBy, OrganizationStatus status)
    {
        LegalName = legalName;
        CommercialName = commercialName;
        Ruc = ruc;
        CreatedBy = createdBy;
        Status = status;
    }
    
    public Organization(LegalName legalName , CommercialName commercialName, RUC ruc, PersonId createdBy)
    {
        LegalName = legalName;
        CommercialName = commercialName;
        Ruc = ruc;
        CreatedBy = createdBy;
    }
    
    public Organization(LegalName legalName , RUC ruc, PersonId createdBy, OrganizationStatus status)
    {
        LegalName = legalName;
        CommercialName = new CommercialName("");
        Ruc = ruc;
        CreatedBy = createdBy;
        Status = status;
    }

    public void EditNames(LegalName legalName, CommercialName commercialName)
    {
        LegalName = legalName;
        CommercialName = commercialName;
    }

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
}