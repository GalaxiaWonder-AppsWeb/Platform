using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class PersonRepository(AppDbContext context) : BaseRepository<Person>(context), IPersonRepository
{
    public bool ExistsByEmail(EmailAddress email)
    {
        return Context.Set<Person>().Any(person => person.Email.Equals(email));
    }
    
    public bool ExistsByPhone(PhoneNumber phone)
    {
        return Context.Set<Person>().Any(person => person.Phone.Equals(phone));
    }
}