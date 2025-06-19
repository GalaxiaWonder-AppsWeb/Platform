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

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Organization endpoints")]
public class OrganizationController(
    IOrganizationQueryService organizationQueryService,
    IOrganizationCommandService organizationCommandService) : ControllerBase
{
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
}