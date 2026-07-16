using MediatR;

namespace Auth.Application.Features.Users.Commands;
public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : IRequest;