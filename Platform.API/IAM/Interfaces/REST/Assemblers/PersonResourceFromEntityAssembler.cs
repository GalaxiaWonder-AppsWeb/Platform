using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

/// <summary>
///     Assembler responsible for converting <see cref="Person"/> domain entities
///     into <see cref="PersonResource"/> DTOs.
/// </summary>
public class PersonResourceFromEntityAssembler
{
    /// <summary>
    ///     Maps a <see cref="Person"/> entity into a <see cref="PersonResource"/> DTO.
    /// </summary>
    /// <param name="person">The domain entity representing a person.</param>
    /// <returns>
    ///     A <see cref="PersonResource"/> containing the person's ID, name, email, and phone number.
    /// </returns>
    public static PersonResource ToResourceFromEntity(Person person)
    {
        return new PersonResource(person.Id, person.Name.FirstName, person.Name.LastName, person.Email.Address,
            person.Phone.Phone);
    }
}