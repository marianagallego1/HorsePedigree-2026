using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Repositories;

public class EquinoRepository : Repository<Equino>, IEquinoRepository
{
    public EquinoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Equino?> GetByIdWithRelationsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(e => e.Estado)
            .Include(e => e.Criadero)
            .Include(e => e.TipoDePaso)
            .Include(e => e.Propietario)
            .Include(e => e.Padre)
            .Include(e => e.Madre)
            .FirstOrDefaultAsync(e => e.EquinoId == id, cancellationToken);
    }

    public async Task<Equino?> GetByIdForUpdateAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(e => e.Estado)
            .FirstOrDefaultAsync(e => e.EquinoId == id, cancellationToken);
    }
}
