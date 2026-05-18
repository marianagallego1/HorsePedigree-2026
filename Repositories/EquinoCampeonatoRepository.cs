using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class EquinoCampeonatoRepository : Repository<EquinoCampeonato>, IEquinoCampeonatoRepository
{
    public EquinoCampeonatoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
