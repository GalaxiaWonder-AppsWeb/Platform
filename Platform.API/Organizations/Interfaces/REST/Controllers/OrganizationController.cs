using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.Organizations.Domain.Model.Commands;
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
    [AllowAnonymous]
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
    [AllowAnonymous]
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
    [AllowAnonymous]
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
}