using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

/// <summary>
///     Assembler responsible for converting <see cref="UserAccount"/> domain entities
///     into <see cref="AuthenticatedUserAccountResource"/> DTOs.
/// </summary>
public class AuthenticatedUserResourceFromEntityAssembler
{
    /// <summary>
    ///     Maps a <see cref="UserAccount"/> entity and a JWT token into an <see cref="AuthenticatedUserAccountResource"/>.
    /// </summary>
    /// <param name="userAccount">The user account domain entity.</param>
    /// <param name="token">The JWT token generated for the authenticated user.</param>
    /// <returns>
    ///     An instance of <see cref="AuthenticatedUserAccountResource"/> containing the mapped user data and token.
    /// </returns>
    public static AuthenticatedUserAccountResource ToResourceFromEntity(
        UserAccount userAccount, string token)
    {
        return new AuthenticatedUserAccountResource(userAccount.Id, userAccount.Username.Username, userAccount.UserType.Name.ToString(), token, userAccount.PersonId.personId);
    }
}