using HorsePedigree_2026.Constants;
using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorsePedigree_2026.Controllers;

[ApiController]
[Route("api/v1/equinos")]
public class EquinosController : ControllerBase
{
    private readonly IEquinoService _equinoService;

    public EquinosController(IEquinoService equinoService)
    {
        _equinoService = equinoService;
    }

    /// <summary>
    /// Listado de equinos con filtros opcionales por nombre, propietario y estado.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EquinoListItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<EquinoListItemResponse>>> GetAllAsync(
        [FromQuery] EquinoFiltrosQuery filtros,
        CancellationToken cancellationToken)
    {
        var equinos = await _equinoService.GetAllAsync(filtros, cancellationToken);
        return Ok(equinos);
    }

    /// <summary>
    /// Registra un nuevo caballo.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EquinoResponse>> CreateAsync(
        [FromBody] CreateEquinoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var equino = await _equinoService.CreateAsync(request, cancellationToken);
        return CreatedAtRoute(EquinoByIdRoute, new { id = equino.EquinoId }, equino);
    }

    private const string EquinoByIdRoute = "GetEquinoById";

    /// <summary>
    /// Consulta la ficha de un caballo por id.
    /// </summary>
    [HttpGet("{id:long}", Name = EquinoByIdRoute)]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoResponse>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var equino = await _equinoService.GetByIdAsync(id, cancellationToken);
        return Ok(equino);
    }

    /// <summary>
    /// Línea genealógica del caballo: padre y madre; abuelos paternos y maternos si están en BD.
    /// </summary>
    [HttpGet("{id:long}/genealogia")]
    [ProducesResponseType(typeof(EquinoGenealogiaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoGenealogiaResponse>> GetGenealogiaAsync(
        long id,
        CancellationToken cancellationToken)
    {
        var genealogia = await _equinoService.GetGenealogiaAsync(id, cancellationToken);
        return Ok(genealogia);
    }

    /// <summary>
    /// Actualiza parcialmente la información de un caballo. Solo es necesario enviar
    /// los campos que se desean modificar; los omitidos conservan su valor actual.
    /// </summary>
    [HttpPut("{id:long}")]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoResponse>> UpdateAsync(
        long id,
        [FromBody] UpdateEquinoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var equino = await _equinoService.UpdateAsync(id, request, cancellationToken);
        return Ok(equino);
    }

    /// <summary>
    /// Cambia el estado del caballo. Si el estado es fallecido, registra la fecha de fallecimiento
    /// y el caballo deja de aparecer en listados de vivos (clasificación por estado_id).
    /// </summary>
    [HttpPatch("{id:long}/estado")]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoResponse>> CambiarEstadoAsync(
        long id,
        [FromBody] CambiarEstadoEquinoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var equino = await _equinoService.CambiarEstadoAsync(id, request, cancellationToken);
        return Ok(equino);
    }

    /// <summary>
    /// Elimina un equino. Solo se permite si no tiene campeonatos ni descendientes registrados.
    /// </summary>
    [HttpDelete("{id:long}")]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _equinoService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
