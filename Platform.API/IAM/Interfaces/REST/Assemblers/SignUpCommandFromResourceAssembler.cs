using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

public class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password, resource.UserType, resource.FirstName, resource.LastName, resource.Email, resource?.Phone);
    }
}