using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Application.Features.Users.Commands;

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
}