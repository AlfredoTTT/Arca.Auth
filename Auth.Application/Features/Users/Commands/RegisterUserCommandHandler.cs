using Auth.Application.Interfaces.Security;
using Auth.Domain.ValueObjects;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Users.Commands;

public class RegisterUserCommandHandler(IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand>
{

    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    //TODO: Inyectar el repositorio de usuarios (IUserRepository) para persistir el usuario
    public Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
       var email = new Email(request.Email);
        var password = new Password(request.Password); // Lanza excepción si es inválido

        // // 2. Verificamos si existe (usando el contrato de la interfaz)
        // if (await _userRepository.GetByEmailAsync(email.Value) != null)
        //     throw new UserAlreadyExistsException(email.Value);

        // 3. Hasheamos
        var hash = _passwordHasher.Hash(password.Value);

        // 4. Persistimos
        var user = new User(email, hash, request.RoleId);
        // await _userRepository.AddAsync(user);
        return Task.CompletedTask;
    }
}