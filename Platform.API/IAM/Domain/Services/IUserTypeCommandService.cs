using Platform.API.IAM.Domain.Model.Commands;
using System.Threading.Tasks;

namespace Platform.API.IAM.Domain.Services
{
    /// <summary>
    ///     Application service interface for handling commands related to user types.
    /// </summary>
    public interface IUserTypeCommandService
    {
        /// <summary>
        ///     Handles the command to seed or initialize user types in the system.
        /// </summary>
        /// <param name="command">The command containing the seed data for user types.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Handle(SeedUserTypeCommand command);
    }
}