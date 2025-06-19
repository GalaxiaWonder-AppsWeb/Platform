using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Domain.Model.Aggregates;
/**
 * <summary>
 *  The user account aggregate
 * </summary>
 * <remarks>
 *  This class is used to represent a user account
 * </remarks>
 */
public partial class UserAccount(string username, string passwordHash)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserAccount"/> class with default values.
    /// </summary>
    public UserAccount() : this(string.Empty, string.Empty) {}
    
    /// <summary>
    ///     The unique identifier of the user account.
    /// </summary>
    public long Id { get; }
    
    /// <summary>
    ///     The username used for authentication.
    /// </summary>
    public UserName Username { get; private set; } = new UserName(username);
    
    /// <summary>
    ///     The hashed password used for authentication.
    /// </summary>
    /// <remarks>
    ///     This property is ignored during JSON serialization.
    /// </remarks>
    [JsonIgnore] public Password PasswordHash { get; private set; } = new Password(passwordHash);
    
    /// <summary>
    ///     Gets or sets the type of user (e.g., Client, Contractor, Organization).
    /// </summary>
    public UserType UserType { get; private set; }
    
    /// <summary>
    ///     Gets or sets the identifier of the person associated with this user account.
    /// </summary>
    public PersonId PersonId { get; private set; }

    /// <summary>
    ///     Gets or sets the raw long value of the associated person identifier (not mapped to database).
    /// </summary>
    [NotMapped]
    public long PersonIdValue
    {
        get => PersonId.personId;
        private set => PersonId = new PersonId(value);
    }
    
    /**
     * <summary>
     *  Update the username
     * </summary>
     * <param name="username"> The new username</param>
     * <returns> The updated user </returns>
     */
    public UserAccount UpdateUsername(string username)
    {
        Username = new UserName(username);
        return this;
    }
    
    /**
     * <summary>
     *  Update the password hash
     * </summary>
     * <param name="passwordHash"> The new password </param>
     * <returns> The updated user </returns>
     */
    public UserAccount UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = new Password(passwordHash);
        return this;
    }
    
    /**
     * <summary>
     *  Assigns the associated person identifier to this account.
     * </summary>
     * <param name="personId"> The person linked to the account</param>
     */
    public void AssignPersonId(long personId)
    {
        this.PersonId = new PersonId(personId);
    }
    
    /// <summary>
    ///     Sets the user type for this account.
    /// </summary>
    /// <param name="userType">The user type to assign.</param>
    public void SetUserType(UserType userType)
    {
        this.UserType = userType;
    }
}