using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class CampeonatoService : ServiceBase<Campeonato>, ICampeonatoService
{
    public CampeonatoService(ICampeonatoRepository repository) : base(repository)
    {
    }
}
