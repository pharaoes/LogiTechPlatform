using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogiTech.Services.Identity.Application.Authentication.Commands.Register;

namespace LogiTech.Services.Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterCommandHandler _registerHandler;

    public AuthController(RegisterCommandHandler registerHandler)
    {
        _registerHandler = registerHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _registerHandler.Handle(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.ErrorMessage, errors = result.Errors });
        }

        return Ok(result.Value);
    }
}
