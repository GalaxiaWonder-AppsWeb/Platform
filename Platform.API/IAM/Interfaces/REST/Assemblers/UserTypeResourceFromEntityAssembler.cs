using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

/// <summary>
///     Assembler responsible for converting <see cref="UserType"/> domain entities
///     into <see cref="UserTypeResource"/> DTOs for external consumption.
/// </summary>
public class UserTypeResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="UserType"/> entity into a <see cref="UserTypeResource"/> DTO.
    /// </summary>
    /// <param name="userType">The domain entity representing the user type.</param>
    /// <returns>
    ///     A <see cref="UserTypeResource"/> containing the ID and name of the user type.
    /// </returns>
    public static UserTypeResource ToResourceFromEntity(UserType userType)
    {
        return new UserTypeResource(userType.Id, userType.Name.ToString());
    }
}