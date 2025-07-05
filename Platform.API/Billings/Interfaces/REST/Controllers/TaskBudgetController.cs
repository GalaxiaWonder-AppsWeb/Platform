using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.Billings.Domain.Model.Queries;
using Platform.API.Billings.Domain.Services;
using Platform.API.Billings.Interfaces.REST.Assemblers;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.Billings.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Task Budget endpoints")]
public class TaskBudgetController(
    ITaskBudgetQueryService taskBudgetQueryService) : ControllerBase
{
    
    [HttpGet("projects/{projectId}/total-task-budget")]
    [SwaggerOperation(
        Summary = "Get Total Task Budget by Project ID",
        Description = "Retrieve the total task budget for a specific project",
        OperationId = "task-budget-get-total-by-project-id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Total task budget retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Project not found")]
    public async Task<IActionResult> GetTotalTaskBudget(long projectId)
    {
        var query = new GetTotalTasksBudgetByProjectIdQuery(projectId);
        var total = await taskBudgetQueryService.Handle(query);
        var resource = MoneyResourceFromEntityAssembler.ToResourceFromEntity(total);
        return Ok(resource);
    }
}