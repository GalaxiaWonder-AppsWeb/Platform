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

    public OrganizationStatus Status { get; private set; }

    private readonly List<long> _organizationMemberIds = new();
    private readonly List<long> _organizationInvitationIds = new();

    public Organization()
    {
    }

    public Organization(LegalName legalName, CommercialName commercialName, RUC ruc, PersonId createdBy)
    {
        LegalName = legalName;
        CommercialName = commercialName;
        Ruc = ruc;
        CreatedBy = createdBy;
        Status = new OrganizationStatus(OrganizationStatuses.ACTIVE);
    }

    public void EditNames(LegalName legalName, CommercialName commercialName)
    {
        LegalName = legalName;
        CommercialName = commercialName;
    }

    public void Deactivate()
    {
        Status = new OrganizationStatus(OrganizationStatuses.INACTIVE);
    }

    public void Activate()
    {
        Status = new OrganizationStatus(OrganizationStatuses.ACTIVE);
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