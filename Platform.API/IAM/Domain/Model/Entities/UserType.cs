using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Domain.Model.Entities;

public class UserType
{
    public long Id { get; private set; }

    public UserTypes Name { get; private set; }

    public UserType()
    {
    }

    public UserType(UserTypes name)
    {
        Name = name;
    }

    
}