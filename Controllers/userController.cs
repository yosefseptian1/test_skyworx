using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace skyworx.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class userController : ControllerBase
{
    private readonly IUserService _userService;
    public userController(IUserService userService){
        this._userService = userService;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUser(){
        try
        {
            var result = await _userService.GetAllUser();
            return Ok(result);
        }
        catch (System.Exception)
        {
            throw;
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> BuatUser([FromBody] buatUser param){
        try
        {
            var (result, message) = await _userService.BuatUser(param);
            return Ok(new {response = result, message = message});
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUser([FromBody] updateUser param){
        try
        {
            var (result, message) = await _userService.UpdateUser(param);
            return Ok(new {response = result, message = message});
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpDelete("{userid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUser(short userid){
        try
        {
            var (result, message) = await _userService.DeleteUser(userid);
            return Ok(new {response = result, message = message});
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
