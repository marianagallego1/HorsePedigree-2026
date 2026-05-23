using HorsePedigree_2026.DTOs.Auth;

namespace HorsePedigree_2026.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<AuthenticatedUserResponse> GetCurrentUserAsync(long usuarioId, CancellationToken cancellationToken = default);
}
