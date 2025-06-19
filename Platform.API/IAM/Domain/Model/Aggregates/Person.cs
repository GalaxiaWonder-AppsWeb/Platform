using Platform.API.IAM.Domain.Model.Commands;
using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *  Person Aggregate Root
 * </summary>
 * <remarks>
 *  This class represents the Person aggregate root.
 *  It contains the properties and methods to manage the person information. 
 * </remarks>
 */
public partial class Person
{
    public long Id { get; }
    /** Full projectName of the person, encapsulated in a description object */
    public PersonName Name { get; }
    /** Unique email of the person, represented as a description object */
    public EmailAddress Email { get; }
    /** Optional phone number of the person, represented as a description object */
    public PhoneNumber Phone { get; }
    /** Optional professional ID (e.g., CIP/CAP), represented as a description object */
    public ProfessionalId professionalId { get; }
    /**
     * Retrieves the person's full projectName as a single formatted string.
     *
     * <returns> The full project name</returns>
     */
    public string Fullname => Name.FullName;
    /**
     * Retrieves the person's email address as a plain string.
     *
     * <returns> The email address</returns>
     */
    public string EmailAddress => Email.Address;
    /**
     * Retrieves the person's phone number as a plain string.
     *
     * <returns> The phone number</returns>
     */
    public string PhoneNumber => Phone.Phone;
    /**
     * Protected constructor required by JPA.
     * Should not be used directly in application code.
     */
    protected Person() { }
    /**
     * Constructs a new Person instance with required fields.
     *
     * <param name="firstName">firstname the first projectName of the person</param>
     * <param name="lastName">lastname the last projectName of the person</param>
     * <param name="email">email the email of the person, wrapped in a description object</param>
     */
    public Person(string firstName, string lastName, string email)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
    }
    /**
     * Constructs a new Person instance with a <see cref="SignUpCommand"/>.
     *
     * <param name="command">the command object containing signup data</param>
     */
    public Person(SignUpCommand command)
    {
        this.Name = new PersonName(command.FirstName, command.LastName);
        this.Email = new EmailAddress(command.Email);
        if(command.Phone != null) this.Phone = new PhoneNumber(command.Phone);
    }
}