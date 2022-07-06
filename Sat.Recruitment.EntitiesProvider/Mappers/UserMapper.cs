using Sat.Recruitment.EntitiesProvider.Dtos;
using Sat.Recruitment.EntitiesProvider.Enums;
using Sat.Recruitment.EntitiesProvider.Models;
using System;

namespace Sat.Recruitment.EntitiesProvider.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User FromDtoToEntity(UserDto dto)
        {
            var user = new User()
            {
                Address = dto.Address,
                Money = dto.Money,
                Name = dto.Name,
                Phone = dto.Phone,
                UserType = (UserType)Enum.Parse(typeof(UserType), dto.UserType)
            };
            user.Email = NormalizeEmail(dto.Email);

            return user;
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }


        public UserDto FromEntityToDto(User user)
        {
            var dto = new UserDto()
            {
                Address = user.Address,
                Money = user.Money,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email,
                UserType = user.UserType.ToString()
            };
            
            return dto;
        }
    }

    public interface IUserMapper
    {
        User FromDtoToEntity(UserDto dto);
        UserDto FromEntityToDto(User user);
    }

}
