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
    
    public PersonName Name { get; }
    
    public EmailAddress Email { get; }
    
    public PhoneNumber Phone { get; }
    
    public ProfessionalId professionalId { get; }
    
    public string Fullname => Name.FullName;
    
    public string EmailAddress => Email.Address;
    
    public string PhoneNumber => Phone.Phone;

    public Person(string firstName, string lastName, string email)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
    }
}