using Auth.Application.Interfaces.Security;
using Auth.Domain.ValueObjects;
using Auth.Domain.Exceptions;
using Auth.Domain.Entities;
using MediatR;
using Auth.Application.Interfaces.Repositories;

namespace Auth.Application.Features.Users.Commands;

public class RegisterUserCommandHandler(IPasswordHasher passwordHasher, IUserRepository userRepository) : IRequestHandler<RegisterUserCommand>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserRepository _userRepository = userRepository;


    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password); // Lanza excepción si es inválido
            if (await _userRepository.GetByEmailAsync(email) != null)
                throw new UserAlreadyExistsException(email.Value);
        var hash = _passwordHasher.Hash(password.Value);
        var user = new User(email, hash, request.RoleId);
        await _userRepository.AddAsync(user);
        return Unit.Value;
    }

    Task IRequestHandler<RegisterUserCommand>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}