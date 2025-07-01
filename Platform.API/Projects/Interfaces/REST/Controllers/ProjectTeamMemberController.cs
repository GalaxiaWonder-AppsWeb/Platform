using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.Projects.Domain.Services;
using Platform.API.Projects.Interfaces.REST.Assemblers;
using Platform.API.Projects.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Projects.Interfaces.REST.Controllers;

// COLOCAR AUTHORIZE AQUI ES NECESARIO PERO CUANDO SE COLOCA
// NO FUNCIONA EL ENDPOINT, LO CAMBIARE MAS ADELANTE
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Team Member endpoints")]
public class ProjectTeamMemberController(
    IProjectTeamMemberCommandService projectTeamMemberCommandService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Project Team Member",
        Description = "Add a new team member to a project",
        OperationId = "project-team-member-create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project team member created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project team member creation failed")]
    public async Task<IActionResult> CreateProjectTeamMember(
        long projectId, [FromBody] CreateProjectTeamMemberResource resource)
    {
        var createProjectTeamMemberCommand =
            CreateProjectTeamMemberCommandFromResourceAssembler.ToCommandFromResource(projectId, resource);
        var projectTeamMember = await projectTeamMemberCommandService.Handle(createProjectTeamMemberCommand);
        if (projectTeamMember is null)
        {
            return BadRequest("Project team member creation failed.");
        }
        var response = ProjectTeamMemberResourceFromEntityAssembler.ToResourceFromEntity(projectTeamMember);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Project Team Member",
        Description = "Remove a team member from a project",
        OperationId = "project-team-member-delete")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project team member deleted successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project team member deletion failed")]
    public async Task<IActionResult> DeleteProjectTeamMember(long id)
    {
        var command = DeleteProjectTeamMemberCommandFromResourceAssembler.ToCommandFromResource(id);
        await projectTeamMemberCommandService.Handle(command);
        return Ok("Project team member deleted successfully.");
    }
}