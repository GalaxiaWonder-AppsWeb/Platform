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
[SwaggerTag("Available Project endpoints")]
public class ProjectController(
    IProjectCommandService projectCommandService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Project",
        Description = "Create a new project",
        OperationId = "project-create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project created successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project creation failed")]
    public async Task<IActionResult> CreateProject(
        [FromBody] CreateProjectResource resource)
    {
        var createProjectCommand = 
            CreateProjectCommandFromResourceAssembler.ToCommandFromResource(resource);
        var project = await projectCommandService.Handle(createProjectCommand);
        if (project is null)
        {
            return BadRequest("Project creation failed.");
        }
        var response = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(response);
    }
}