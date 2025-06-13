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
[SwaggerTag("Available Person endpoints")]
public class PersonsController(IPersonQueryService personQueryService) : ControllerBase 
{
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