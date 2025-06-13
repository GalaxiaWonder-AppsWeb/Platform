using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Interfaces.REST.Resources;

public class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            resource.Username,
            resource.Password,
            resource.UserType.ToString(),
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource?.Phone);
    }
}