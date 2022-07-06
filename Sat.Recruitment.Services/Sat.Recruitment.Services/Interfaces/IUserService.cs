using Sat.Recruitment.EntitiesProvider.Commons;
using Sat.Recruitment.EntitiesProvider.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Exists(User user);

        Task<User> Add(User user);

        Task<Result> CreateUser(User user);
    }
}