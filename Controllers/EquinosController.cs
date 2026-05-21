using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Services;
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
    /// Registra un nuevo caballo.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EquinoResponse>> CreateAsync(
        [FromBody] CreateEquinoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var equino = await _equinoService.CreateAsync(request, cancellationToken);
        return CreatedAtAction("GetById", new { id = equino.EquinoId }, equino);
    }

    /// <summary>
    /// Consulta la ficha de un caballo por id.
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoResponse>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var equino = await _equinoService.GetByIdAsync(id, cancellationToken);
        return Ok(equino);
    }

    /// <summary>
    /// Actualiza la información de un caballo.
    /// </summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(EquinoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
}
