using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;

namespace Platform.API.IAM.Application.Internal.QueryServices;

public class PersonQueryService(IPersonRepository personRepository) : IPersonQueryService
{
    public async Task<IEnumerable<Person>> Handle(GetAllPersonsQuery query)
    {
        return await personRepository.ListAsync();
    }

}