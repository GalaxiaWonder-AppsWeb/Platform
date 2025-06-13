using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Application.Internal.CommandServices;
public class UserTypeCommandService(
    IUserTypeRepository userTypeRepository,
    IUnitOfWork unitOfWork
) : IUserTypeCommandService
{
    public async Task Handle(SeedUserTypeCommand command)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var enumValue in Enum.GetValues(typeof(UserTypes)))
            {
                var name = enumValue.ToString();
                if (userTypeRepository.ExistsByName(name)) continue;

                var userType = new UserType(Enum.Parse<UserTypes>(name));
                await userTypeRepository.AddAsync(userType);
            }

            await unitOfWork.CompleteAsync();
            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new Exception($"An error occurred while seeding user types: {e.Message}");
        }
    }
}
