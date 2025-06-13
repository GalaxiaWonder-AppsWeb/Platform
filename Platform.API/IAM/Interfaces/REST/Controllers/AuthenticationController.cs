using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Platform.API.IAM.Interfaces.REST.Assemblers;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication endpoints")]
public class AuthenticationController(IUserAccountCommandService userAccountCommandService) : ControllerBase 
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in a user",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserAccountResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userAccountCommandService.Handle(signInCommand);
        var resource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.userAccount,
                authenticatedUser.token);
        return Ok(resource);
    }
    
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign-up",
        Description = "Sign up a new user",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request or user already exists")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        try
        {
            await userAccountCommandService.Handle(signUpCommand);
            return StatusCode(StatusCodes.Status201Created, new { message = "User created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}