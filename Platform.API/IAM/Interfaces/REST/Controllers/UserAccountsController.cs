using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.IAM.Interfaces.REST.Assemblers;
using Platform.API.IAM.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.IAM.Interfaces.REST.Controllers;
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Accounts endpoints")]
public class UserAccountsController(IUserAccountQueryService userAccountQueryService): ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user account by its id",
        Description = "Get a user account by its id",
        OperationId = "GetUserAccountById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user account was found", typeof(UserAccountResource))]
    public async Task<IActionResult> GetUserAccountById(long id)
    {
        var getUserAccountByIdQuery = new GetUserAccountByIdQuery(id);
        var userAccount = await userAccountQueryService.Handle(getUserAccountByIdQuery);
        var userAccountResource = UserAccountResourceFromEntityAssembler.ToResourceFromEntity(userAccount!);
        return Ok(userAccountResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all user accounts",
        Description = "Get all user accounts",
        OperationId = "GetAllUserAccounts")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user accounts were found", typeof(IEnumerable<UserAccountResource>))]
    public async Task<IActionResult> GetAllUserAccounts()
    {
        var getAllUserAccountsQuery = new GetAllUsersAccountQuery();
        var userAccounts = await userAccountQueryService.Handle(getAllUserAccountsQuery);
        var userAccountResources = userAccounts.Select(UserAccountResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userAccountResources);
    }
}