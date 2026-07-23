using Auth.Application.Interfaces.Security;
using Auth.Domain.ValueObjects;
using Auth.Domain.Exceptions;
using Auth.Domain.Entities;
using MediatR;
using Auth.Contracts.Events;
using Auth.Domain.Interfaces.Repositories;
using MassTransit;
using Auth.Application.Interfaces.Common;

namespace Auth.Application.Features.Users.Commands;

public class RegisterUserCommandHandler(IPasswordHasher passwordHasher, IUserRepository userRepository, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password); 
            if (await _userRepository.GetByEmailAsync(email) != null)
                throw new UserAlreadyExistsException(email.Value);
        var hash = _passwordHasher.Hash(password.Value);
        var user = new User(email, hash, request.RoleId);
        await _userRepository.AddAsync(user, cancellationToken);
        await _publishEndpoint.Publish(new UserRegistrationInitiatedEvent(
            user.Id,
            user.Email.Value,
            DateTime.UtcNow
        ), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    Task IRequestHandler<RegisterUserCommand>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}