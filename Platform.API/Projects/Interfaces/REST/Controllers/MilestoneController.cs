using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
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
}