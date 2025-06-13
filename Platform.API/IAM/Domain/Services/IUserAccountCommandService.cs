using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Commands;

namespace Platform.API.IAM.Domain.Services;

public interface IUserAccountCommandService
{
    Task<(UserAccount userAccount, string token)> Handle(SignInCommand command);
    
    Task Handle(SignUpCommand command);

}