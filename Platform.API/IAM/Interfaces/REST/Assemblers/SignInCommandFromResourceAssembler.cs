using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

/// <summary>
///     Assembler responsible for converting <see cref="SignInResource"/> DTOs
///     into <see cref="SignInCommand"/> domain commands.
/// </summary>
public class SignInCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SignInResource"/> into a <see cref="SignInCommand"/>.
    /// </summary>
    /// <param name="resource">The resource containing the sign-in data from the client.</param>
    /// <returns>
    ///     A <see cref="SignInCommand"/> containing the user's email and password.
    /// </returns>
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}