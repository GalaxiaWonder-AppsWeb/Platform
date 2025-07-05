using Platform.API.Billings.Domain.Model.Queries;

namespace Platform.API.Billings.Domain.Services;

public interface ITaskBudgetQueryService
{
    Task<decimal> Handle(GetTotalTasksBudgetByProjectIdQuery query);
}