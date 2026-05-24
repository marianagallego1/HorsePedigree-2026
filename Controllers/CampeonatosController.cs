using HorsePedigree_2026.Constants;
using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorsePedigree_2026.Controllers;

[ApiController]
[Route("api/v1/campeonatos")]
public class CampeonatosController : ControllerBase
{
    private readonly ICampeonatoService _campeonatoService;

    public CampeonatosController(ICampeonatoService campeonatoService)
    {
        _campeonatoService = campeonatoService;
    }

    private const string CampeonatoByIdRoute = "GetCampeonatoById";

    /// <summary>
    /// Registra un nuevo campeonato.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(CampeonatoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CampeonatoResponse>> CreateAsync(
        [FromBody] CreateCampeonatoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var campeonato = await _campeonatoService.CreateAsync(request, cancellationToken);
        return CreatedAtRoute(CampeonatoByIdRoute, new { id = campeonato.CampeonatoId }, campeonato);
    }

    /// <summary>
    /// Consulta un campeonato por id.
    /// </summary>
    [HttpGet("{id:long}", Name = CampeonatoByIdRoute)]
    [ProducesResponseType(typeof(CampeonatoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampeonatoResponse>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var campeonato = await _campeonatoService.GetDetalleAsync(id, cancellationToken);
        return Ok(campeonato);
    }
}
