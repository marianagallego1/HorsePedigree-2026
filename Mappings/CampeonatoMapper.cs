using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Mappings;

public static class CampeonatoMapper
{
    public static Campeonato ToEntity(CreateCampeonatoRequest request)
    {
        return new Campeonato
        {
            Nombre = request.Nombre.Trim(),
            FechaCampeonato = request.FechaCampeonato,
            Ubicacion = request.Ubicacion.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(request.Descripcion)
                ? null
                : request.Descripcion.Trim(),
            Nivel = request.Nivel.Trim()
        };
    }

    public static CampeonatoResponse ToResponse(Campeonato campeonato)
    {
        return new CampeonatoResponse
        {
            CampeonatoId = campeonato.CampeonatoId,
            Nombre = campeonato.Nombre,
            FechaCampeonato = campeonato.FechaCampeonato,
            Ubicacion = campeonato.Ubicacion,
            Descripcion = campeonato.Descripcion,
            Nivel = campeonato.Nivel
        };
    }
}
