using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Domain.Model.Queries;
using Platform.API.Change.Domain.Repositories;
using Platform.API.Change.Domain.Services;

namespace Platform.API.Change.Application.Internal.QueryServices;

public class ChangeProcessQueryService(
    IChangeProcessRepository changeProcessRepository): IChangeProcessQueryService
{
    public async Task<ChangeProcess?> Handle(GetChangeProcessByProjectIdQuery query)
    {
        return await changeProcessRepository.FindByProjectId(query.ProjectId);
    }
}