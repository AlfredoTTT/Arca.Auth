using MediatR;

namespace Auth.Application.Features.Users.Commands;
public record RegisterUserCommand(string Email, string Password, Guid RoleId): IRequest;