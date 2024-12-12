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

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserDto>>> GetUserById(string id)
        {
            var listUsers = await _businessUser.GetUserById(Guid.Parse(id));
            return Ok(listUsers);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUser()
        {
            var listUsers = await _businessUser.GetUsers();
            return Ok(listUsers);
        }


        [HttpPost()]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {
            userDto.Id = Guid.NewGuid().ToString();
            var result = await _businessUser.CreateUser(userDto);
            return Ok(result);
        }

        [HttpPost("LogUser")]
        public async Task<ActionResult> LogUser([FromBody] LogDataUserDto loguserDto)
        {
            var response = await _businessUser.LoginUser(loguserDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var response = await _businessUser.UpdateUser(userDto);
            return Ok(response);
        }
    }
}