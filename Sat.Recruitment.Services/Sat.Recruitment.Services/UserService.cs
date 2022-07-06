using Sat.Recruitment.EntitiesProvider.Commons;
using Sat.Recruitment.EntitiesProvider.Dtos;
using Sat.Recruitment.EntitiesProvider.Mappers;
using Sat.Recruitment.EntitiesProvider.Models;
using Sat.Recruitment.Repositories.Interfaces;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IUserMapper _userMapper;
        public UserService(IUserRepository userRepository, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public async Task<bool> Exists(User user)
        {
            var users = await _userRepository.GetUsers();
            var searchedUser = users.FirstOrDefault(u => (u.Email == user.Email || u.Phone == user.Phone) || (u.Name == user.Name && u.Address == user.Address));
            return (!(searchedUser is null));
        }

        public async Task<User> Add(User user)
        {
            var userByType = user.GetUserByType();
            userByType.CalculateMoney();

            return await _userRepository.Add(userByType);
        }

        public async Task<Result> CreateUser(User user)
        {
            try
            {
                if (await Exists(user) is false)
                {
                    var usr = await Add(user);
                    var dto = _userMapper.FromEntityToDto(usr);
                    return new Result<UserDto> { IsSuccess = true, Data = dto, Message = "User Created" };
                }
            }
            catch (Exception)
            {
                return new Result { IsSuccess = false, Message = "Internal Server Error" };
            }

            return new Result { IsSuccess = false, Message = "User is duplicated" };
        }
    }
}
