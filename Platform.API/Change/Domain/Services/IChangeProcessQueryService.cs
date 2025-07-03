using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Domain.Model.Queries;

namespace Platform.API.Change.Domain.Services;

public interface IChangeProcessQueryService
{
    Task<ChangeProcess?> Handle(GetChangeProcessByProjectIdQuery query);
}