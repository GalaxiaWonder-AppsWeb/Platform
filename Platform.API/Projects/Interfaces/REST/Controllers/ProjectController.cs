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

    [HttpPatch("{id}/name")]
    [SwaggerOperation(
        Summary = "Update Project Name",
        Description = "Update the name of an existing project",
        OperationId = "project-update-name")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project name updated successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project update failed")]
    public async Task<IActionResult> UpdateProjectName(
        long id, [FromBody] UpdateProjectNameResource resource)
    {
        var command = UpdateProjectNameCommandFromResourceAssembler
            .ToCommandFromResource(id, resource);
        var project = await projectCommandService.Handle(command);
        if (project is null)
        {
            return BadRequest("Project update failed.");
        }
        return Ok(project);
    }
    
    [HttpPatch("{id}/description")]
    [SwaggerOperation(
        Summary = "Update Project Description",
        Description = "Update the description of an existing project",
        OperationId = "project-update-description")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project description updated successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project update failed")]
    public async Task<IActionResult> UpdateProjectDescription(
        long id, [FromBody] UpdateProjectDescriptionResource resource)
    {
        var command = UpdateProjectDescriptionCommandFromResourceAssembler
            .ToCommandFromResource(id, resource);
        var project = await projectCommandService.Handle(command);
        if (project is null)
        {
            return BadRequest("Project update failed.");
        }
        return Ok(project);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Project",
        Description = "Delete an existing project",
        OperationId = "project-delete")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project deleted successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project deletion failed")]
    public async Task<IActionResult> DeleteProject(long id)
    {
        var command = DeleteProjectCommandFromResourceAssembler.ToCommandFromResource(id);
        await projectCommandService.Handle(command);
        return Ok("Project deleted successfully");
    }
}