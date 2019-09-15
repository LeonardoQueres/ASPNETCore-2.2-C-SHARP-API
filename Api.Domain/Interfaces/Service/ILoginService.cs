using Api.Domain.DTO;
using Api.Domain.Entities;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Service
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDTO user);
    }
}
