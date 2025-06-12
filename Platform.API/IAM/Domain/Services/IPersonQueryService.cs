using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;

namespace Platform.API.IAM.Domain.Services;

public interface IPersonQueryService
{
    Task<IEnumerable<Person>> Handle(GetAllPersonsQuery query);
}