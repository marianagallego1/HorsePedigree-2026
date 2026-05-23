using HorsePedigree_2026.DTOs.Auth;
using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Helpers;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.GetByUsernameOrEmailAsync(request.Username, cancellationToken);
        if (usuario is null || !PasswordHelper.Verify(request.Password, usuario.Password))
        {
            throw new UnauthorizedException("Credenciales inválidas.");
        }

        var (token, expiresAtUtc) = _jwtTokenService.CreateToken(usuario);

        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAtUtc,
            User = MapToAuthenticatedUser(usuario)
        };
    }

    public async Task<AuthenticatedUserResponse> GetCurrentUserAsync(
        long usuarioId,
        CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.GetByIdWithRolAsync(usuarioId, cancellationToken);
        if (usuario is null)
        {
            throw new UnauthorizedException("La sesión ya no es válida.");
        }

        return MapToAuthenticatedUser(usuario);
    }

    private static AuthenticatedUserResponse MapToAuthenticatedUser(Usuario usuario)
    {
        return new AuthenticatedUserResponse
        {
            UsuarioId = usuario.UsuarioId,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Username = usuario.Username,
            Email = usuario.Email,
            RolId = usuario.RolId,
            RolDescripcion = usuario.Rol?.Descripcion ?? string.Empty
        };
    }
}
