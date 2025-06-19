using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Application.Internal.CommandServices;

/// <summary>
///     Application service that handles commands related to user type management,
///     such as seeding initial user type values from the <see cref="UserTypes"/> enumeration.
/// </summary>
/// <param name="userTypeRepository">The repository used to manage user type entities.</param>
/// <param name="unitOfWork">The unit of work used to handle transactional consistency.</param>
public class UserTypeCommandService(
    IUserTypeRepository userTypeRepository,
    IUnitOfWork unitOfWork
) : IUserTypeCommandService
{
    /// <summary>
    ///     Handles the seeding of user types based on the <see cref="UserTypes"/> enum.
    ///     Only non-existing values are inserted. The operation is executed transactionally.
    /// </summary>
    /// <param name="command">The command triggering the seeding operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">
    ///     Thrown if an error occurs during the transaction while creating user types.
    /// </exception>
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
