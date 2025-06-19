using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.IAM.Interfaces.REST.Assemblers;
using Platform.API.IAM.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.IAM.Interfaces.REST.Controllers;

/// <summary>
///     Controller responsible for handling operations related to <see cref="UserAccount"/> entities,
///     such as retrieving a single account or listing all user accounts.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Accounts endpoints")]
public class UserAccountsController(IUserAccountQueryService userAccountQueryService): ControllerBase
{
    /// <summary>
    ///     Retrieves a user account by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user account to retrieve.</param>
    /// <returns>
    ///     A <see cref="UserAccountResource"/> if found; otherwise, appropriate error response.
    /// </returns>
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
    
    /// <summary>
    ///     Retrieves a list of all user accounts in the system.
    /// </summary>
    /// <returns>
    ///     A collection of <see cref="UserAccountResource"/> items.
    /// </returns>
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