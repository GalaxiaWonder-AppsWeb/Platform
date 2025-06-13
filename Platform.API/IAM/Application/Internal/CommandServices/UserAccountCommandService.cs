using Platform.API.IAM.Application.Internal.OutboundServices;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;
using Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Application.Internal.CommandServices;

public class UserAccountCommandService(
    IUserAccountRepository userAccountRepository,
    IPersonRepository personRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) 
    : IUserAccountCommandService
{
    public async Task<(UserAccount userAccount, string token)> Handle(SignInCommand command)
    {
        var userAccount = await userAccountRepository.FindByEmailAsync(command.Email);
        if (userAccount == null || !hashingService.VerifyPassword(command.Password, userAccount.PasswordHash.HashedPassword))
            throw new Exception("Invalid email or password");
        var token = tokenService.GenerateToken(userAccount);
        
        return (userAccount, token);
    }

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
            userAccount.SetUserType(Enum.Parse<UserTypes>(command.UserType));

            await userAccountRepository.AddAsync(userAccount);
            await unitOfWork.CompleteAsync(); // Guarda el UserAccount

            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new Exception($"An error occurred during sign up: {e.Message}");
        }
    }


}