using Sat.Recruitment.EntitiesProvider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> Add(User user);
    }
}
