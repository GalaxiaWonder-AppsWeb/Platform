using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

public class PersonResourceFromEntityAssembler
{
    public static PersonResource ToResourceFromEntity(Person person)
    {
        return new PersonResource(person.Id, person.Name.FirstName, person.Name.LastName, person.Email.Address,
            person.Phone.Phone);
    }
}