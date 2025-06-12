using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

public class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserAccountResource ToResourceFromEntity(
        UserAccount userAccount, string token)
    {
        return new AuthenticatedUserAccountResource(userAccount.Id, userAccount.Username.ToString(), token);
    }
}