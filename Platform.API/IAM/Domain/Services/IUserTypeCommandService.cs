using Platform.API.IAM.Domain.Model.Commands;

namespace Platform.API.IAM.Domain.Services;

public interface IUserTypeCommandService
{
    void Handle(SeedUserTypeCommand command);
}