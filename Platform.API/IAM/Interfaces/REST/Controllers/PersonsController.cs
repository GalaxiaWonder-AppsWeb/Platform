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
///     Controller responsible for handling operations related to <see cref="Person"/> entities,
///     such as retrieving a single person or listing all persons.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Person endpoints")]
public class PersonsController(IPersonQueryService personQueryService) : ControllerBase 
{
    /// <summary>
    ///     Retrieves a person by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>
    ///     A <see cref="PersonResource"/> if found; otherwise, a 404 Not Found or appropriate status.
    /// </returns>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a person by its id",
        Description = "Get a person by its id",
        OperationId = "GetPersonById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The person was found", typeof(PersonResource))]
    public async Task<IActionResult> GetPersonById(long id)
    {
        var getPersonByIdQuery = new GetPersonByIdQuery(id);
        var person = await personQueryService.Handle(getPersonByIdQuery);
        var personResource = PersonResourceFromEntityAssembler.ToResourceFromEntity(person!);
        return Ok(personResource);
    }
    
    /// <summary>
    ///     Retrieves a list of all persons registered in the system.
    /// </summary>
    /// <returns>
    ///     A collection of <see cref="PersonResource"/> items.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all persons",
        Description = "Get all persons",
        OperationId = "GetAllPersons")]
    [SwaggerResponse(StatusCodes.Status200OK, "The persons were found", typeof(IEnumerable<PersonResource>))]
    public async Task<IActionResult> GetAllPersons()
    {
        var getAllPersonsQuery = new GetAllPersonsQuery();
        var persons = await personQueryService.Handle(getAllPersonsQuery);
        var personResources = persons.Select(PersonResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(personResources);
    }
}