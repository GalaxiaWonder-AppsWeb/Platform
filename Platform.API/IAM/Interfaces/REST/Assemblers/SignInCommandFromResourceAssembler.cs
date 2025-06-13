using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Interfaces.REST.Resources;

namespace Platform.API.IAM.Interfaces.REST.Assemblers;

public class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}