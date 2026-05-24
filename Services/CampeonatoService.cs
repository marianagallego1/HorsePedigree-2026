using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Mappings;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class CampeonatoService : ServiceBase<Campeonato>, ICampeonatoService
{
    private readonly ICampeonatoRepository _campeonatoRepository;

    public CampeonatoService(ICampeonatoRepository repository) : base(repository)
    {
        _campeonatoRepository = repository;
    }

    public async Task<CampeonatoResponse> CreateAsync(
        CreateCampeonatoRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidarCamposObligatorios(request);

        var campeonato = CampeonatoMapper.ToEntity(request);
        await _campeonatoRepository.AddAsync(campeonato, cancellationToken);

        return CampeonatoMapper.ToResponse(campeonato);
    }

    public async Task<CampeonatoResponse> GetDetalleAsync(long id, CancellationToken cancellationToken = default)
    {
        var campeonato = await _campeonatoRepository.GetByIdAsync(id, cancellationToken);
        if (campeonato is null)
        {
            throw new NotFoundException(nameof(Campeonato), id);
        }

        return CampeonatoMapper.ToResponse(campeonato);
    }

    private static void ValidarCamposObligatorios(CreateCampeonatoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            throw new BusinessException("El nombre es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Ubicacion))
        {
            throw new BusinessException("La ubicación es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.Nivel))
        {
            throw new BusinessException("El nivel es obligatorio.");
        }
    }
}
