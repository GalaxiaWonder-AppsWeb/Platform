﻿using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.IAM.Interfaces.REST.Assemblers;
using Platform.API.IAM.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Platform.API.IAM.Interfaces.REST.Controllers;

/// <summary>
///     Controller responsible for handling operations related to <see cref="UserType"/> entities,
///     such as retrieving a specific user type or listing all available user types.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Types endpoints")]
public class UserTypeController(IUserTypeQueryService userTypeQueryService) : ControllerBase
{
    /// <summary>
    ///     Retrieves a user type by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user type to retrieve.</param>
    /// <returns>
    ///     A <see cref="UserTypeResource"/> if found; otherwise, a not found or error response.
    /// </returns>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user type by its id",
        Description = "Get a user type by its id",
        OperationId = "GetUserTypeById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user type was found", typeof(UserTypeResource))]
    public async Task<IActionResult> GetUserTypeById(long id)
    {
        var getUserTypeByIdQuery = new GetUserTypeByIdQuery(id);
        var userType = await userTypeQueryService.Handle(getUserTypeByIdQuery);
        var userTypeResource = UserTypeResourceFromEntityAssembler.ToResourceFromEntity(userType!);
        return Ok(userTypeResource);
    }

    /// <summary>
    ///     Retrieves a list of all user types available in the system.
    /// </summary>
    /// <returns>
    ///     A collection of <see cref="UserTypeResource"/> items.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all user types",
        Description = "Get all user types",
        OperationId = "GetAllUserTypes")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user types were found", typeof(IEnumerable<UserTypeResource>))]
    public async Task<IActionResult> GetAllUserTypes()
    {
        var getAllUserTypesQuery = new GetAllUserTypesQuery();
        var userTypes = await userTypeQueryService.Handle(getAllUserTypesQuery);
        var userTypeResources = userTypes.Select(UserTypeResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userTypeResources);
    }
}