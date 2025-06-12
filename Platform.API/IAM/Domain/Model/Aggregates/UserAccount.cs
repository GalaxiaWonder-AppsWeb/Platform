using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    public UserAccount() : this(string.Empty, string.Empty) {}
    public int Id { get; }
    public UserName Username { get; private set; } = new UserName(username);
    [JsonIgnore] public Password PasswordHash { get; private set; } = new Password(passwordHash);
    
    public UserTypes UserType { get; private set; }
    
    public PersonId PersonId { get; private set; }

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
    
    public void SetUserType(UserTypes userType)
    {
        this.UserType = userType;
    }

}