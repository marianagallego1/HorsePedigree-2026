using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class EquinoCampeonatoService : ServiceBase<EquinoCampeonato>, IEquinoCampeonatoService
{
    public EquinoCampeonatoService(IEquinoCampeonatoRepository repository) : base(repository)
    {
    }
}
