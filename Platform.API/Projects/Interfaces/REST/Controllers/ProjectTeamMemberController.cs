using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.Projects.Domain.Services;
using Platform.API.Projects.Interfaces.REST.Assemblers;
using Platform.API.Projects.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Projects.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project Team Member endpoints")]
public class ProjectTeamMemberController(
    IProjectTeamMemberCommandService projectTeamMemberCommandService) : ControllerBase
{
    // COLOCAR AUTHORIZE AQUI ES NECESARIO PERO CUANDO SE COLOCA
    // NO FUNCIONA EL ENDPOINT, LO CAMBIARE MAS ADELANTE
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
}