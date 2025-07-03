using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
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
    
    [HttpPatch("{id}")]
    [SwaggerOperation(
        Summary = "Update a Task",
        Description = "Update an existing task",
        OperationId = "task-update")]
    [SwaggerResponse(StatusCodes.Status200OK, "Task updated successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Task update failed")]
    public async Task<IActionResult> UpdateTask(long id, 
        [FromBody] UpdateTaskResource resource)
    {
        var updateTaskCommand = UpdateTaskCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var task = await taskCommandService.Handle(updateTaskCommand);
        if (task is null)
        {
            return BadRequest("Task update failed.");
        }
        var response = await TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
        
        return Ok(response);
    }
    
    [HttpGet("by-milestone-id/{milestoneId}")]
    [SwaggerOperation(
        Summary = "Get Tasks by Milestone ID",
        Description = "Retrieve all tasks associated with a specific milestone",
        OperationId = "task-get-by-milestone-id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tasks retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Tasks retrieval failed")]
    public async Task<IActionResult> GetAllTasksByMilestoneId(long milestoneId)
    {
        var query = new GetAllTasksByMilestoneIdQuery(milestoneId);
        var tasks = await taskQueryService.Handle(query);
        var resources = new List<TaskResource>();
        foreach (var task in tasks)
        {
            var resource = await TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
            resources.Add(resource);
        }
        
        return Ok(resources);
    }
    
    [HttpGet("by-person-id-and-milestone-id/{personId}/{milestoneId}")] //esto no tendria que llamarse asi, es un antipatron.
    [SwaggerOperation(
        Summary = "Get Tasks by Person ID and Milestone ID",
        Description = "Retrieve all tasks associated with a specific person and milestone",
        OperationId = "task-get-by-person-id-and-milestone-id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tasks retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Tasks retrieval failed")]
    public async Task<IActionResult> GetAllTasksByPersonIdAndMilestoneId(long personId, long milestoneId)
    {
        var query = new GetAllTasksByPersonIdAndMilestoneIdQuery(personId, milestoneId);
        var tasks = await taskQueryService.Handle(query);
        var resources = new List<TaskResource>();
        foreach (var task in tasks)
        {
            var resource = await TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
            resources.Add(resource);
        }
        
        return Ok(resources);
    }
}