using Core.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (model.Email == "admin@gmail.com" && model.Password == "admin")
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
