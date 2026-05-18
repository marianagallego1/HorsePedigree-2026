using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class EquinoRepository : Repository<Equino>, IEquinoRepository
{
    public EquinoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
