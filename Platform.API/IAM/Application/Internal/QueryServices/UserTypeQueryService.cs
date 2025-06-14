using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;

namespace Platform.API.IAM.Application.Internal.QueryServices;

public class UserTypeQueryService(IUserTypeRepository userTypeRepository) : IUserTypeQueryService
{
    public async Task<IEnumerable<UserType>> Handle(GetAllUserTypesQuery query)
    {
        return await userTypeRepository.ListAsync();
    }

    public async Task<UserType> Handle(GetUserTypeByIdQuery query)
    {
        return await userTypeRepository.FindByIdAsync(query.Id);
    }
}