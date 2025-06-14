using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

public class UserAccountResourceFromEntityAssembler
{
    public static UserAccountResource ToResourceFromEntity(UserAccount userAccount)
    {
        return new UserAccountResource(userAccount.Id, userAccount.Username.Username,
            userAccount.UserType.Name.ToString(), userAccount.PersonId.personId.ToString());
    }
}