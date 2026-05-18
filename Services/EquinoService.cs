using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class EquinoService : ServiceBase<Equino>, IEquinoService
{
    public EquinoService(IEquinoRepository repository) : base(repository)
    {
    }
}
