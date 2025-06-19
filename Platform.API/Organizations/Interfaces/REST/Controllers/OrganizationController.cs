using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.Organizations.Application.Internal.CommandServices;
using Platform.API.Organizations.Domain.Model.Commands;
using Platform.API.Organizations.Domain.Model.Entities;
using Platform.API.Organizations.Domain.Model.Queries;
using Platform.API.Organizations.Domain.Services;
using Platform.API.Organizations.Interfaces.REST.Assemblers;
using Platform.API.Organizations.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Organizations.Interfaces.REST.Controllers;

/// <summary>
/// Controller that provides RESTful endpoints to manage organizations,
/// including creation, update, deletion, invitations, and membership operations.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Organization endpoints")]
public class OrganizationController(
    IOrganizationQueryService organizationQueryService,
    IOrganizationCommandService organizationCommandService) : ControllerBase
{
    /// <summary>
    /// Creates a new organization with legal name, commercial name, RUC, and owner.
    /// </summary>
    [HttpPost("create-organization")]
    [SwaggerOperation(
        Summary = "Create Organization",
        Description = "Create Organization with legal name, commercial name, ruc and owner",
        OperationId = "CreateOrganization")]
    [SwaggerResponse(200, "Organization created", typeof(OrganizationResource))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    [SwaggerResponse(409, "Conflict", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> CreateOrganization(
        [FromBody] CreateOrganizationResource createOrganizationResource)
    {
        var createOrganizationCommand =
            CreateOrganizationCommandFromResourceAssembler.ToCommandFromResource(createOrganizationResource);
        var organization = await organizationCommandService.Handle(createOrganizationCommand);
        var resource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization);
        return Ok(resource);
    }

    /// <summary>
    /// Deletes an organization by its identifier.
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Eliminate Organization",
        Description = "Eliminate Organization with id",
        OperationId = "EliminateOrganization")]
    [SwaggerResponse(200, "Organization eliminated", typeof(OrganizationResource))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    public async Task<IActionResult> DeleteOrganization(long id)
    {
        var deleteOrganizationCommand = new DeleteOrganizationCommand(id);
        await organizationCommandService.Handle(deleteOrganizationCommand);
        return Ok("Organization eliminated");
    }

    /// <summary>
    /// Updates the legal and/or commercial name of an organization.
    /// </summary>
    [HttpPatch("{id}")]
    [SwaggerOperation(
        Summary = "Update Commercial name and/or Legal Name of an organization",
        Description = "Update commercial name and legal name of an organization",
        OperationId = "UpdateOrganization")]
    [SwaggerResponse(200, "Organization updated", typeof(OrganizationResource))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    public async Task<IActionResult> Handle(long id, 
        [FromBody] UpdateOrganizationResource resource)
    {
        var command = new UpdateOrganizationCommand(id, resource.LegalName, resource.CommercialName);
        var organization = await organizationCommandService.Handle(command);
        return Ok(organization);
    }

    /// <summary>
    /// Gets the details of an organization by ID.
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Organization by Id",
        Description = "Get Organization by Id",
        OperationId = "GetOrganizationById")]
    [SwaggerResponse(200, "Organization found", typeof(OrganizationResource))]
    [SwaggerResponse(404, "Organization not found", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> GetOrganizationById(long id)
    {
        var query = new GetOrganizationByIdQuery(id);
        var organization = await organizationQueryService.Handle(query);
        if (organization == null) return NotFound($"Organization with ID {id} not found");
        var resource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization);
        return Ok(resource);
    }
    
    /// <summary>
    /// Gets all organizations where the specified person is a member.
    /// </summary>
    [HttpGet("by-member-person-id/{id}")]
    [SwaggerOperation(
        Summary = "Get an Organization by Person Id",
        Description = "Get an Organization by Person Id",
        OperationId = "GetOrganizationByMemberPersonId")]
    [SwaggerResponse(200, "Organization found", typeof(IEnumerable<OrganizationResource>))]
    [SwaggerResponse(404, "Organization not found", typeof(string))]
    [SwaggerResponse(500, "Internal Server Error", typeof(string))]
    public async Task<IActionResult> GetOrganizationsByMemberPersonId(long id)
    {
        var query = new GetAllOrganizationsByMemberPersonIdQuery(id);
        var organizations = await organizationQueryService.Handle(query);

        var resources = organizations
            .Select(OrganizationResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();

        return Ok(resources);
    }
    
    /// <summary>
    /// Sends an invitation to a person to join an organization by email.
    /// </summary>
    [HttpPost("invitations")]
    [SwaggerOperation(
        Summary = "Invite a person to an organization",
        Description = "Invite a person to an organization",
        OperationId = "InviteToOrganization")]
    [SwaggerResponse(200, "Organization invited", typeof(OrganizationResource))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    public async Task<IActionResult> InvitePersonToOrganization(
        [FromBody] InvitePersonToOrganizationResource resource)
    {
        try
        {
            var command = new InvitePersonToOrganizationByEmailCommand(resource.OrganizationId, resource.Email);
            var (organization, invitation, profileDetails) = await organizationCommandService.Handle(command);

            var response = InvitePersonToOrganizationAssembler.ToResource(organization, invitation, profileDetails);
            return CreatedAtAction(nameof(InvitePersonToOrganization), response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    /// <summary>
    /// Accepts an organization invitation by invitation ID.
    /// </summary>
    [HttpPatch("invitations/{id}/accept")]
    [SwaggerOperation(
        Summary = "Accept an organization invitation",
        Description = "Accept an invitation to join an organization by invitation ID",
        OperationId = "AcceptOrganizationInvitation")]
    [SwaggerResponse(200, "Invitation accepted")]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    public async Task<IActionResult> AcceptInvitation([FromRoute] long id)
    {
        try
        {
            var command = new AcceptInvitationCommand(id);
            var (organization, invitation, profileDetails) = await organizationCommandService.Handle(command);

            var response = InvitePersonToOrganizationAssembler.ToResource(organization, invitation, profileDetails);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Rejects an organization invitation by invitation ID.
    /// </summary>
    [HttpPatch("invitations/{id}/reject")]
    [SwaggerOperation(
        Summary = "Reject an organization invitation",
        Description = "Reject an invitation to join an organization by invitation ID",
        OperationId = "RejectOrganizationInvitation")]
    [SwaggerResponse(200, "Invitation rejected")]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    public async Task<IActionResult> RejectInvitation([FromRoute] long id)
    {
        var command = new RejectInvitationCommand(id);
        var (organization, invitation, profileDetails) = await organizationCommandService.Handle(command);

        var response = InvitePersonToOrganizationAssembler.ToResource(organization, invitation, profileDetails);
        return Ok(response);
    }

    /// <summary>
    /// Gets all invitations associated with an organization by its ID.
    /// </summary>
    [HttpGet("{id}/invitations")]
    [SwaggerOperation(
        Summary = "Get all organization invitations",
        Description = "Get all organization invitations",
        OperationId = "GetAllOrganizationInvitations"
        )]
    [SwaggerResponse(200, "All organization invitations", typeof(IEnumerable<OrganizationInvitation>))]
    [SwaggerResponse(404, "Organization not found", typeof(string))]
    public async Task<IActionResult> GetAllInvitationsByOrganizationId(long id)
    {
        var query = new GetAllInvitationsByOrganizationIdQuery(id);
        var invitations = await organizationQueryService.Handle(query);

        if (!invitations.Any())
            return NotFound($"No invitations found for organization with ID {id}");

        var resources = invitations
            .Select(tuple => OrganizationInvitationWithProfileAssembler.ToResource(tuple.Item1, tuple.Item2));

        return Ok(resources);
    }

    /// <summary>
    /// Gets all members of an organization by its ID.
    /// </summary>
    [HttpGet("{id}/members")]
    [SwaggerOperation(
        Summary = "Get all organization members",
        Description = "Get all members of the specified organization along with their profile details",
        OperationId = "GetAllOrganizationMembers"
    )]
    [SwaggerResponse(200, "All organization members")]
    [SwaggerResponse(404, "Organization not found or has no members", typeof(string))]
    public async Task<IActionResult> GetAllMembersByOrganizationId(long id)
    {
        var query = new GetAllMembersByOrganizationIdQuery(id);
        var members = await organizationQueryService.Handle(query);

        if (!members.Any())
            return NotFound($"No members found for organization with ID {id}");

        var resources = members
            .Select(tuple => OrganizationMemberWithProfileAssembler.ToResource(tuple.Item1, tuple.Item2));

        return Ok(resources);
    }
    
    /// <summary>
    /// Deletes a member from the organization by their membership ID.
    /// </summary>
    [HttpDelete("members/{id}")]
    [SwaggerOperation(
        Summary = "Remove a member from an organization",
        Description = "Removes a specific member from the organization using their ID",
        OperationId = "RemoveOrganizationMember")]
    [SwaggerResponse(204, "Member removed successfully")]
    [SwaggerResponse(404, "Member not found", typeof(string))]
    [SwaggerResponse(400, "Bad Request", typeof(string))]
    public async Task<IActionResult> DeleteOrganizationMember(long id)
    {
        var command = new DeleteOrganizationMemberCommand(id);
        await organizationCommandService.Handle(command);
        return Ok("Member eliminated");
    }
    
    /// <summary>
    /// Gets all invitations received by a specific person.
    /// </summary>
    [HttpGet("persons/{id}/invitations")]
    [SwaggerOperation(
        Summary = "Get all invitations by person ID",
        Description = "Returns all invitations received by a specific person, with organization and status info.",
        OperationId = "GetAllInvitationsByPersonId"
    )]
    [SwaggerResponse(200, "List of invitations")]
    [SwaggerResponse(404, "No invitations found")]
    public async Task<IActionResult> GetAllInvitationsByPersonId(long id)
    {
        var query = new GetAllInvitationsByPersonIdQuery(id);
        var result = await organizationQueryService.Handle(query);

        if (!result.Any())
            return NotFound($"No invitations found for person with ID {id}");

        var resources = result.Select(tuple =>
            OrganizationInvitationWithDetailsAssembler.ToResource(tuple.Item1, tuple.Item2, tuple.Item3));

        return Ok(resources);
    }
}