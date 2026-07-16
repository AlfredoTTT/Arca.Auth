using MediatR;
using Auth.Application.Interfaces.Repositories;
using Auth.Domain.Exceptions;
using Auth.Application.Interfaces.Security;
using Auth.Application.Interfaces.Common;
using Auth.Domain.ValueObjects;

namespace Auth.Application.Features.Users.Commands;
public class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService): IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(new Email(request.Email))
                   ?? throw new InvalidCredentialsException();
        if (string.IsNullOrEmpty(user.PasswordHash))
            throw new InvalidCredentialsException();
        if (!passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException();
        return tokenService.GenerateToken(user.Id, user.Email.Value);
    }
}