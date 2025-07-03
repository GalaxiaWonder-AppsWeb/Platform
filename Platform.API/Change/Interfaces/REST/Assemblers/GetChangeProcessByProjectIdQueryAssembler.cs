using Platform.API.Change.Domain.Model.Queries;

namespace Platform.API.Change.Interfaces.REST.Assemblers;

public class GetChangeProcessByProjectIdQueryAssembler
{
    public static GetChangeProcessByProjectIdQuery ToQueryFromProjectId(long projectId)
    {
        return new GetChangeProcessByProjectIdQuery(projectId);
    }
}