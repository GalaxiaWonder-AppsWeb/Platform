using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

public class UserTypeResourceFromEntityAssembler
{
    public static UserTypeResource ToResourceFromEntity(UserType userType)
    {
        return new UserTypeResource(userType.Id, userType.Name.ToString());
    }
}