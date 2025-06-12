using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;

namespace Platform.API.IAM.Domain.Services;

public interface IUserAccountQueryService
{
    Task<UserAccount?> Handle(GetUserAccountByIdQuery query);
}