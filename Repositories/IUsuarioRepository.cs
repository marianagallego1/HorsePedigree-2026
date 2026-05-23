using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken = default);

    Task<Usuario?> GetByIdWithRolAsync(long id, CancellationToken cancellationToken = default);
}
