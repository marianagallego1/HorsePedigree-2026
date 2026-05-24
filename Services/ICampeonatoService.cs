using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Services;

public interface ICampeonatoService : IService<Campeonato>
{
    Task<CampeonatoResponse> CreateAsync(CreateCampeonatoRequest request, CancellationToken cancellationToken = default);
    Task<CampeonatoResponse> GetDetalleAsync(long id, CancellationToken cancellationToken = default);
}
