using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Model.Queries;
using Platform.API.Projects.Domain.Services;
using Platform.API.Projects.Interfaces.REST.Assemblers;
using Platform.API.Projects.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Projects.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Project endpoints")]
public class ProjectController(
    IProjectCommandService projectCommandService,
    IProjectQueryService projectQueryService,
    ProjectResourceFromEntityAssembler projectResourceFromEntityAssembler) : ControllerBase
{
    [HttpPost("[controller]")]
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

        var response = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(response);
    }

    [HttpPatch("[controller]/{id}/name")]
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
        var response = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);

        return Ok(response);
    }

    [HttpPatch("[controller]/{id}/description")]
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
        var response = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);

        return Ok(response);
    }

    [HttpPatch("[controller]/{projectId}/status")]
    [SwaggerOperation(
        Summary = "Update Project Status",
        Description = "Update the status of an existing project",
        OperationId = "project-update-status")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project status updated successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project update failed")]
    public async Task<IActionResult> UpdateProjectStatus(
        long projectId, [FromBody] UpdateProjectStatusResource resource)
    {
        var command = UpdateProjectStatusCommandFromResourceAssembler.ToCommandFromResource(projectId, resource);
        var project = await projectCommandService.Handle(command);
        if (project is null)
        {
            return BadRequest("Project update failed.");
        }
        var response = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(response);
    }
    
    [HttpPatch("[controller]/{projectId}/date-range")]
    [SwaggerOperation(
        Summary = "Update Project Date Range",
        Description = "Update the date range of an existing project",
        OperationId = "project-update-date-range")]
    [SwaggerResponse(StatusCodes.Status200OK, "Project date range updated successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Project update failed")]
    public async Task<IActionResult> UpdateProjectDateRange(
        long projectId, [FromBody] UpdateProjectDateRangeResource resource)
    {
        var command = UpdateProjectDateRangeCommandFromResourceAssembler.ToCommandFromResource(projectId, resource);
        var project = await projectCommandService.Handle(command);
        if (project is null)
        {
            return BadRequest("Project update failed.");
        }
        var response = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(response);
    }

    [HttpGet("[controller]/contracting-entity/{id}")]
    [SwaggerOperation(
        Summary = "Get Projects by Contracting Entity Id",
        Description = "Retrieve projects by a contracting entity Id",
        OperationId = "projects-get-by-contracting-entity-id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Projects retrieved successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Projects not found")]
    public async Task<IActionResult> GetProjectsByContractingEntityId(long id)
    {
        var query = new GetAllProjectsByContractingEntityIdQuery(id);
        var projects = await projectQueryService.Handle(query);
        var resources = new List<ProjectResource>();
        foreach (var project in projects)
        {
            var resource = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);
            resources.Add(resource);
        }

        return Ok(resources);
    }

    [HttpDelete("[controller]/{id}")]
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

    [HttpGet("organization/{organizationId}/team-members/{personId}/projects")]
    [SwaggerOperation(
        Summary = "Get Projects by personId and organization",
        Description = "Retrieve projects by a personId and organization",
        OperationId = "projects-get-by-person-id-organization")]
    [SwaggerResponse(StatusCodes.Status200OK, "Projects retrieved successfully", typeof(ProjectResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Projects not found")]
    public async Task<IActionResult> GetProjectsByPersonId(long organizationId, long personId)
    {
        var query = new GetAllProjectsByTeamMemberPersonIdQuery(
            personId, organizationId);
        var projects = await projectQueryService.Handle(query);
        var resources = new List<ProjectResource>();
        foreach (var project in projects)
        {
            var resource = await projectResourceFromEntityAssembler.ToResourceFromEntity(project);
            resources.Add(resource);
        }

        return Ok(resources);

    }
}