using Platform.API.IAM.Domain.Model.Commands;
using System.Threading.Tasks;

namespace Platform.API.IAM.Domain.Services
{
    public interface IUserTypeCommandService
    {
        Task Handle(SeedUserTypeCommand command);
    }
}