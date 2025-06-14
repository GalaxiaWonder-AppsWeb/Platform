using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Domain.Services;

public interface IUserTypeQueryService
{
    Task<IEnumerable<UserType>> Handle(GetAllUserTypesQuery query);
    Task<UserType> Handle(GetUserTypeByIdQuery query);
}