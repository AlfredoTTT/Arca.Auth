using Auth.Application.Interfaces.Security;
using Auth.Domain.Exceptions;
using Auth.Domain.Interfaces.Repositories;
using Auth.Domain.ValueObjects;
using MediatR;

namespace Auth.Application.Features.Users.Commands;
public class ChangePasswordCommandHandler(
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher) 
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId) 
                   ?? throw new Exception("Usuario no encontrado");

        if (!passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
            throw new InvalidCredentialsException();
        var newPasswordValidator = new Password(request.NewPassword);
        var hashedValue = passwordHasher.Hash(newPasswordValidator.Value);
        user.UpdatePassword(hashedValue);
        await userRepository.UpdateAsync(user);
    }
}