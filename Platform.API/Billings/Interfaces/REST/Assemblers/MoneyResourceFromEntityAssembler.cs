using Platform.API.Billings.Interfaces.REST.Resources;
using Platform.API.Shared.Domain.Model.ValueObjects;

namespace Platform.API.Billings.Interfaces.REST.Assemblers;

public class MoneyResourceFromEntityAssembler
{
    public static MoneyResource ToResourceFromEntity(decimal amount)
    {

        return new MoneyResource(amount, "USD");
    }
}