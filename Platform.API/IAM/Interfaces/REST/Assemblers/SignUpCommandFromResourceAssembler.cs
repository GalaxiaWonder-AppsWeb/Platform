using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Interfaces.REST.Resources;

/// <summary>
///     Assembler responsible for converting <see cref="SignUpResource"/> DTOs
///     into <see cref="SignUpCommand"/> domain commands.
/// </summary>
public class SignUpCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="SignUpResource"/> into a <see cref="SignUpCommand"/> to execute the registration process.
    /// </summary>
    /// <param name="resource">The resource containing user-provided registration data.</param>
    /// <returns>
    ///     A <see cref="SignUpCommand"/> containing all necessary information to create a new user account.
    /// </returns>
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Username,
            resource.Password,
            resource.UserType.ToString(),
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource?.Phone);
    }
}