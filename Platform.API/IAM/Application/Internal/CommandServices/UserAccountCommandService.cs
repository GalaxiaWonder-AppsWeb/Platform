using Platform.API.IAM.Application.Internal.OutboundServices;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Application.Internal.CommandServices;

/// <summary>
///     Application service that handles user account-related commands,
///     such as sign-in and sign-up processes.
/// </summary>
/// <param name="userAccountRepository">The repository for accessing user account entities.</param>
/// <param name="userTypeRepository">The repository for retrieving user type entities.</param>
/// <param name="personRepository">The repository for managing person entities.</param>
/// <param name="tokenService">The service for generating authentication tokens.</param>
/// <param name="hashingService">The service for hashing and verifying passwords.</param>
/// <param name="unitOfWork">The unit of work for managing transactional operations.</param>
public class UserAccountCommandService(
    IUserAccountRepository userAccountRepository,
    IUserTypeRepository userTypeRepository,
    IPersonRepository personRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) 
    : IUserAccountCommandService
{
    /// <summary>
    ///     Handles the sign-in command by verifying credentials and generating a JWT token.
    /// </summary>
    /// <param name="command">The sign-in command containing the email and password.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the authenticated <see cref="UserAccount"/> and the JWT token string.
    /// </returns>
    /// <exception cref="Exception">Thrown if the credentials are invalid.</exception>
    public async Task<(UserAccount userAccount, string token)> Handle(SignInCommand command)
    {
        var userAccount = await userAccountRepository.FindByEmailAsync(command.Email);
        if (userAccount == null || !hashingService.VerifyPassword(command.Password, userAccount.PasswordHash.HashedPassword))
            throw new Exception("Invalid email or password");
        var token = tokenService.GenerateToken(userAccount);
        
        return (userAccount, token);
    }

    /// <summary>
    ///     Handles the sign-up command by creating a new person and user account with hashed password,
    ///     and associating the correct user type. The entire operation is transactional.
    /// </summary>
    /// <param name="command">The sign-up command containing user and personal data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">
    ///     Thrown if the username or email is already in use, or if any error occurs during the registration process.
    /// </exception>
    public async Task Handle(SignUpCommand command)
    {
        if (userAccountRepository.ExistsByUserName(new UserName(command.Username)))
            throw new Exception($"Username {command.Username} is already taken");

        if (personRepository.ExistsByEmail(new EmailAddress(command.Email)))
            throw new Exception($"Email {command.Email} is already taken");
        
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var person = new Person(command);
            await personRepository.AddAsync(person);
            await unitOfWork.CompleteAsync();

            var hashedPassword = hashingService.HashPassword(command.Password);
            var userAccount = new UserAccount(command.Username, hashedPassword);

            userAccount.AssignPersonId(person.Id);
            var userTypeEntity = await userTypeRepository.FindByNameAsync(command.UserType.ToString());

            if (userTypeEntity == null)
                throw new Exception($"User type {command.UserType} not found");

            userAccount.SetUserType(userTypeEntity);

            await userAccountRepository.AddAsync(userAccount);
            await unitOfWork.CompleteAsync();

            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new Exception($"An error occurred during sign up: {e.Message}");
        }
    }
}