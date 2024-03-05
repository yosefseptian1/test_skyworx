using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace skyworx.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class authController : ControllerBase
{
    private readonly IUserService _userService;
    public authController(IUserService userService){
        this._userService = userService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] registerUser param){
        try
        {
            var (result, message) = await _userService.register(param);
            return Ok(new {response = result, message = message});
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] registerUser param){
        try
        {
            var (result, message, token) = await _userService.login(param);
            return Ok(new {response = result, message = message, token = token});
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
