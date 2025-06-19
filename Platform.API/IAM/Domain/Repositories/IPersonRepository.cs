using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

public interface IPersonRepository : IBaseRepository<Person>
{
    bool ExistsByEmail(EmailAddress email);
    
    bool ExistsByPhone(PhoneNumber phone);
    
    new Task<Person?> FindByEmailAsync(string email);
}