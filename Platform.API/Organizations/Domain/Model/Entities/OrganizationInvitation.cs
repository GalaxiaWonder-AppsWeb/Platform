using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Organizations.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

public partial class OrganizationInvitation
{
    public long Id { get; private set; }

    public OrganizationId OrganizationId { get; private set; }

    public PersonId PersonId { get; private set; }

    public PersonName InvitedBy { get; private set; }

    public OrganizationInvitationStatus Status { get; private set; }

    public OrganizationInvitation()
    {
    }

    public OrganizationInvitation(OrganizationId organizationId, PersonId personId, PersonName invitedBy)
    {
        OrganizationId = organizationId;
        PersonId = personId;
        Status = new OrganizationInvitationStatus(OrganizationInvitationStatuses.PENDING);
        InvitedBy = invitedBy;
    }

    public void Accept()
    {
        if (Status.Name != OrganizationInvitationStatuses.PENDING)
            throw new InvalidOperationException("The invitation has already been answered.");

        Status = new OrganizationInvitationStatus(OrganizationInvitationStatuses.ACCEPTED);
    }

    public void Reject()
    {
        if (Status.Name != OrganizationInvitationStatuses.PENDING)
            throw new InvalidOperationException("The invitation has already been answered.");

        Status = new OrganizationInvitationStatus(OrganizationInvitationStatuses.REJECTED);
    }
}