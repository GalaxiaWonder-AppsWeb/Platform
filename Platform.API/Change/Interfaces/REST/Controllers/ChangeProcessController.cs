using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.Change.Domain.Services;
using Platform.API.Change.Interfaces.REST.Assemblers;
using Platform.API.Change.Interfaces.REST.Resources;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Change.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Change Process endpoints")]
public class ChangeProcessController(
    IChangeProcessQueryService changeProcessQueryService,
    IChangeProcessCommandService changeProcessCommandService) : ControllerBase
{
    [HttpPost("by-project-id/{projectId}")]
    [SwaggerOperation(
        Summary = "Create a Change Process",
        Description = "Create a new change process for a project",
        OperationId = "change-process-create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Change process created successfully", typeof(ChangeProcessResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Change process creation failed")]
    public async Task<IActionResult> CreateChangeProcess(
        long projectId, CreateChangeProcessResource resource)
    {
        var command = CreateChangeProcessComandFromResourceAssembler.ToCommandFromResource(projectId, resource);
        var changeProcess = await changeProcessCommandService.Handle(command);
        if (changeProcess is null)
        {
            return BadRequest("Change process creation failed.");
        }
        var response = await ChangeProcessResourceFromEntityAssembler.ToResourceFromEntity(changeProcess);
        return Ok(response);
    }

    [HttpPatch("{changeProcessId}")]
    [SwaggerOperation(
        Summary = "Update a Change Process",
        Description = "Update an existing change process",
        OperationId = "change-process-update")]
    [SwaggerResponse(StatusCodes.Status200OK, "Change process updated successfully", typeof(ChangeProcessResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Change process update failed")]
    public async Task<IActionResult> RespondToChangeProcess(long changeProcessId,
        RespondToChangeProcessResource resource)
    {
        var command = RespondToChangeProcessCommandFromResourceAssembler.ToCommandFromResource(changeProcessId, resource);
        var changeProcess = await changeProcessCommandService.Handle(command);
        if (changeProcess is null)
        {
            return BadRequest("Change process response failed.");
        }
        var response = await ChangeProcessResourceFromEntityAssembler.ToResourceFromEntity(changeProcess);
        return Ok(response);
    }
    
    [HttpGet("by-project-id/{projectId}")]
    [SwaggerOperation(
        Summary = "Get Change Process by Project ID",
        Description = "Retrieve the change process associated with a specific project ID",
        OperationId = "change-process-get-by-project-id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Change process retrieved successfully", typeof(ChangeProcessResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Change process not found for the given project ID")]
    public async Task<IActionResult> GetChangeProcessByProjectId(long projectId)
    {
        var query = GetChangeProcessByProjectIdQueryAssembler.ToQueryFromProjectId(projectId);
        var changeProcess = await changeProcessQueryService.Handle(query);
        if (changeProcess is null)
        {
            return NotFound($"Change process for project ID {projectId} not found.");
        }
        var response = await ChangeProcessResourceFromEntityAssembler.ToResourceFromEntity(changeProcess);
        return Ok(response);
    }
}