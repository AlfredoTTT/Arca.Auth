namespace Auth.Application.Interfaces.Common;

public interface ITokenService
{
    string GenerateToken(Guid userId, string email);
}