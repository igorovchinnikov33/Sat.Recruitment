using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.EntitiesProvider.Mappers;
using Sat.Recruitment.EntitiesProvider.Dtos;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;

        public UsersController(IUserMapper userMapper, IUserService userService)
        {
            _userMapper = userMapper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = _userMapper.FromDtoToEntity(dto);

                var result = await _userService.CreateUser(user);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return Conflict(result);
            }
            return BadRequest(ModelState);
        }
    }
}
