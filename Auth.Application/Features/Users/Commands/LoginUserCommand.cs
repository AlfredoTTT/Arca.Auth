using MediatR;

namespace Auth.Application.Features.Users.Commands;
public record LoginUserCommand(string Email, string Password) : IRequest<string>; // Retornará un Token (string)