using Backend.Application.Interfaces;
using Backend.Application.Models.Requests;
using Backend.Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserRes>> CreateUser(CreateUserReq user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ValidateUserRes>> ValidateUser(ValidateUserReq req)
        {
            var result = await _userService.ValidateUser(req);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllActiveUsersRes>> GetAllActiveUsers()
        {
            var result = await _userService.GetAllActiveUsers();
            return Ok(result);
        }
    }
}
