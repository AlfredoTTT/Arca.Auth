using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Application.Features.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        // Enviamos el comando a través de MediatR
        await _mediator.Send(command);
        
        return Ok(new { message = "Usuario registrado exitosamente" });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        // Enviamos el comando a través de MediatR
        var token = await _mediator.Send(command);
        
        return Ok(new { Token = token });
    }

    [Authorize] 
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        
        // comando con el ID obtenido del token
        await _mediator.Send(command with { UserId = Guid.Parse(userId!) });
        
        return Ok(new { Message = "Contraseña actualizada" });
    }
}