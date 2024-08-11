using Microsoft.AspNetCore.Mvc;
using TaskManagement.Users.Application.Dto;
using TaskManagement.Users.Application.Interfaces;

namespace TaskManagement.Users.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _businessUser;


        public UserController(IUserBL businessUser)
        {
            _businessUser = businessUser;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUser()
        {
            var listUsers = await _businessUser.GetUsers();
            return Ok(listUsers);
        }


        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {
            await _businessUser.CreateUser(userDto);
            return Ok();
        }

        [HttpPost("LogUser")]
        public async Task<ActionResult> LogUser([FromBody] LogDataUserDto loguserDto)
        {
            var response = await _businessUser.LoginUser(loguserDto);
            return Ok(response);
        }
    }
}