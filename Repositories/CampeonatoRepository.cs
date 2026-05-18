using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class CampeonatoRepository : Repository<Campeonato>, ICampeonatoRepository
{
    public CampeonatoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
