using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Commands;

namespace Platform.API.IAM.Domain.Services;

/// <summary>
///     Application service interface for handling user account-related commands.
/// </summary>
public interface IUserAccountCommandService
{
    /// <summary>
    ///     Handles the sign-in process for a user and returns the authenticated user account along with a JWT token.
    /// </summary>
    /// <param name="command">The sign-in command containing credentials.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a tuple with the authenticated <see cref="UserAccount"/> and a JWT token string.
    /// </returns>
    Task<(UserAccount userAccount, string token)> Handle(SignInCommand command);
    
    /// <summary>
    ///     Handles the sign-up process to register a new user account in the system.
    /// </summary>
    /// <param name="command">The sign-up command containing user registration details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Handle(SignUpCommand command);

}