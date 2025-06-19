using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

/// <summary>
///     Assembler responsible for converting <see cref="UserAccount"/> domain entities
///     into <see cref="UserAccountResource"/> DTOs for external consumption.
/// </summary>
public class UserAccountResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="UserAccount"/> entity into a <see cref="UserAccountResource"/> DTO.
    /// </summary>
    /// <param name="userAccount">The domain entity representing the user account.</param>
    /// <returns>
    ///     A <see cref="UserAccountResource"/> containing the user's ID, username, type, and associated person ID.
    /// </returns>
    public static UserAccountResource ToResourceFromEntity(UserAccount userAccount)
    {
        return new UserAccountResource(userAccount.Id, userAccount.Username.Username,
            userAccount.UserType.Name.ToString(), userAccount.PersonId.personId.ToString());
    }
}