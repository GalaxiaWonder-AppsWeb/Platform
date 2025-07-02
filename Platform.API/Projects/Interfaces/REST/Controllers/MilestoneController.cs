using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Queries;
using Platform.API.Projects.Domain.Services;
using Platform.API.Projects.Interfaces.REST.Assemblers;
using Platform.API.Projects.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Projects.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Milestone endpoints")]
public class MilestoneController(
    IMilestoneCommandService milestoneCommandService,
    IMilestoneQueryService milestoneQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Milestone",
        Description = "Create a new milestone for a project",
        OperationId = "milestone-create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Milestone created successfully", typeof(MilestoneResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Milestone creation failed")]
    public async Task<IActionResult> CreateMilestone(
        [FromBody] CreateMilestoneResource resource)
    {
        var command = CreateMilestoneCommandFromResourceAssembler.ToCommandFromResource(resource);
        var milestone = await milestoneCommandService.Handle(command);
        if (milestone is null)
        {
            return BadRequest("Milestone creation failed.");
        }

        var response = await MilestoneResourceFromEntityAssembler.ToResourceFromEntity(milestone);
        return Ok(response);
    }
    
    [HttpPatch("{id}/name")]
    [SwaggerOperation(
        Summary = "Update Milestone Name",
        Description = "Update the name of an existing milestone",
        OperationId = "milestone-update-name")]
    [SwaggerResponse(StatusCodes.Status200OK, "Milestone name updated successfully", typeof(MilestoneResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Milestone update failed")]
    public async Task<IActionResult> UpdateMilestoneName(long id, [FromBody] UpdateMilestoneNameResource resource)
    {
        var command = UpdateMilestoneNameCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var milestone = await milestoneCommandService.Handle(command);
        if (milestone is null)
        {
            return BadRequest("Milestone update failed.");
        }

        var response = await MilestoneResourceFromEntityAssembler.ToResourceFromEntity(milestone);
        return Ok(response);
    }
    
    [HttpPatch("{id}/description")]
    [SwaggerOperation(
        Summary = "Update Milestone Description",
        Description = "Update the description of an existing milestone",
        OperationId = "milestone-update-description")]
    [SwaggerResponse(StatusCodes.Status200OK, "Milestone description updated successfully", typeof(MilestoneResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Milestone update failed")]
    public async Task<IActionResult> UpdateMilestoneDescription(long id, [FromBody] UpdateMilestoneDescriptionResource resource)
    {
        var command = UpdateMilestoneDescriptionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var milestone = await milestoneCommandService.Handle(command);
        if (milestone is null)
        {
            return BadRequest("Milestone update failed.");
        }

        var response = await MilestoneResourceFromEntityAssembler.ToResourceFromEntity(milestone);
        return Ok(response);
    }
    
    [HttpPatch("{id}/date")]
    [SwaggerOperation(
        Summary = "Update Milestone Date Range",
        Description = "Update the date range of an existing milestone",
        OperationId = "milestone-update-date-range")]
    [SwaggerResponse(StatusCodes.Status200OK, "Milestone date range updated successfully", typeof(MilestoneResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Milestone update failed")]
    public async Task<IActionResult> UpdateMilestoneDateRange(long id, [FromBody] UpdateMilestoneDateRangeResource resource)
    {
        var command = UpdateMilestoneDateRangeCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var milestone = await milestoneCommandService.Handle(command);
        if (milestone is null)
        {
            return BadRequest("Milestone update failed.");
        }

        var response = await MilestoneResourceFromEntityAssembler.ToResourceFromEntity(milestone);
        return Ok(response);
    }

    [HttpGet("by-project/{projectId}")]
    [SwaggerOperation(
        Summary = "Get Milestones by Project ID",
        Description = "Retrieve all milestones associated with a specific project ID",
        OperationId = "milestone-get-by-project-id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Milestones retrieved successfully", typeof(IEnumerable<MilestoneResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No milestones found for the specified project ID")]
    public async Task<IActionResult> GetMilestonesByProjectId(long projectId)
    {
        var query = new GetAllMilestonesByProjectIdQuery(projectId);
        var milestones = await milestoneQueryService.Handle(query);
        var resourceTasks = milestones.Select(m => MilestoneResourceFromEntityAssembler.ToResourceFromEntity(m));
        var resources = await Task.WhenAll(resourceTasks);
        return Ok(resources);

    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Milestone",
        Description = "Delete an existing milestone",
        OperationId = "milestone-delete")]
    [SwaggerResponse(StatusCodes.Status200OK, "Milestone deleted successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Milestone deletion failed")]
    public async Task<IActionResult> DeleteMilestone(long id)
    {
        var command = DeleteMilestoneCommandFromResourceAssembler.ToCommandFromResource(id);
        await milestoneCommandService.Handle(command);
        return Ok("Milestone deleted successfully");
    }
}