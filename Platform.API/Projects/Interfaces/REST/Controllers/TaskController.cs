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
[SwaggerTag("Available Task endpoints")]
public class TaskController(
    ITaskCommandService taskCommandService,
    ITaskQueryService taskQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Task",
        Description = "Create a new task",
        OperationId = "task-create")]
    [SwaggerResponse(StatusCodes.Status200OK, "Task created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Task creation failed")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskResource resource)
    {
        var createTaskCommand = CreateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var task = await taskCommandService.Handle(createTaskCommand);
        if (task is null)
        {
            return BadRequest("Task creation failed.");
        }
        var response = await TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        
        return Ok(response);
    }
}