using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;

namespace Platform.API.IAM.Application.Internal.QueryServices;

public class UserAccountQueryService(IUserAccountRepository userAccountRepository) : IUserAccountQueryService
{
    public async Task<UserAccount?> Handle(GetUserAccountByIdQuery query)
    {
        return await userAccountRepository.FindByIdAsync(query.Id);
    }
}