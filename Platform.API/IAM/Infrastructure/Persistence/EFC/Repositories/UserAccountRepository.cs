using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;

namespace Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Repository implementation for managing <see cref="UserAccount"/> entities.
/// </summary>
/// <param name="context">The application's database context.</param>
public class UserAccountRepository(AppDbContext context) : BaseRepository<UserAccount>(context), IUserAccountRepository
{
    /// <summary>
    ///     Determines whether a user account with the specified username exists in the database.
    /// </summary>
    /// <param name="userName">The username to check.</param>
    /// <returns><c>true</c> if a matching user account is found; otherwise, <c>false</c>.</returns>
    public bool ExistsByUserName(UserName userName)
    {
        return Context.Set<UserAccount>()
            .Any(userAccount => userAccount.Username.Username.Equals(userName.Username));
    }

    /// <summary>
    ///     Asynchronously finds a user account by its unique identifier, including its associated user type.
    /// </summary>
    /// <param name="id">The ID of the user account.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserAccount"/> with its user type if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<UserAccount?> FindByIdWithUserTypeAsync(long id)
    {
        return await context.UserAccounts
            .Include(u => u.UserType)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    ///     Asynchronously retrieves all user accounts, including their associated user types.
    /// </summary>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="UserAccount"/> entities with user types included.
    /// </returns>
    public async Task<IEnumerable<UserAccount>> ListWithUserTypeAsync()
    {
        return await context.UserAccounts
            .Include(u => u.UserType)
            .ToListAsync();
    }
    
    /// <summary>
    ///     Asynchronously finds a user account by the email address of its associated person, including user type.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserAccount"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<UserAccount?> FindByEmailAsync(string email)
    {
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Email.Address == email);
        if (person == null) return null;

        return await context.UserAccounts
            .Include(ua => ua.UserType) 
            .FirstOrDefaultAsync(ua => ua.PersonId.personId == person.Id);
    }
}