using CORE.Entites;
using CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWATask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _UserService;
        public AccountsController(IUserService UserService)
        {
            this._UserService = UserService;
        }
        [HttpPost]
        [Route("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromForm]AddNewUser form)
        {
            bool AddCheck =  await _UserService.AddNewUser(form);
            if (AddCheck)
                return Ok(new { StausCode = 200, Message = "User Added Successfully" });
            else
                return BadRequest(new { StausCode = 500, Message = "Failed To Add User" });
        } 
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm]LoginForm form)
        {
            var data =  await _UserService.Login(form);
            if (data == null )
                return NotFound(new { StausCode = 404, Message = "User With this Email Or Password Not Found" });
            else
                return Ok(new { StausCode = 200, Message = "User Founded!", data = data });

        }
    }
}
