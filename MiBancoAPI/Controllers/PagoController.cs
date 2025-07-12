using Microsoft.AspNetCore.Mvc;
using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Request;
using MiBancoAPI.Models.DTOs.Response;
using MiBancoAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MiBancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PagoController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagoController(IPagoService pagoService)
    {
        _pagoService = pagoService;
    }

    /// <summary>
    /// Crea un nuevo pago
    /// </summary>
    /// <param name="pagoDto">Datos del pago a crear</param>
    /// <returns>Pago creado</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Crear nuevo pago", Description = "Registra una nueva transacción de pago")]
    [SwaggerResponse(201, "Pago creado exitosamente", typeof(ApiResponse<Pago>))]
    [SwaggerResponse(400, "Datos inválidos")]
    public async Task<ActionResult<ApiResponse<Pago>>> CrearPago([FromBody] PagoCreateDto pagoDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Pago>.ErrorResult("Datos inválidos",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
            }

            var nuevoPago = await _pagoService.CrearPagoAsync(pagoDto);

            return CreatedAtAction(nameof(ObtenerPagosPorCliente),
                new { dpi = nuevoPago.DPICliente },
                ApiResponse<Pago>.SuccessResult(nuevoPago, "Pago procesado exitosamente"));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<Pago>.ErrorResult(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<Pago>.ErrorResult(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Pago>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }

    /// <summary>
    /// Obtiene todos los pagos de un cliente específico
    /// </summary>
    /// <param name="dpi">DPI del cliente</param>
    /// <returns>Lista de pagos del cliente</returns>
    [HttpGet("cliente/{dpi}")]
    [SwaggerOperation(Summary = "Obtener pagos por cliente", Description = "Retorna el historial de pagos de un cliente")]
    [SwaggerResponse(200, "Lista de pagos", typeof(ApiResponse<List<Pago>>))]
    public async Task<ActionResult<ApiResponse<List<Pago>>>> ObtenerPagosPorCliente(string dpi)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dpi))
            {
                return BadRequest(ApiResponse<List<Pago>>.ErrorResult("El DPI es requerido"));
            }

            var pagos = await _pagoService.ObtenerPagosPorClienteAsync(dpi);
            var total = await _pagoService.ObtenerTotalPagosPorClienteAsync(dpi);

            return Ok(ApiResponse<List<Pago>>.SuccessResult(pagos,
                $"Se encontraron {pagos.Count} pagos. Total: Q{total:N2}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<Pago>>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }

    /// <summary>
    /// Obtiene todos los pagos registrados
    /// </summary>
    /// <returns>Lista de todos los pagos</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obtener todos los pagos", Description = "Retorna la lista completa de pagos registrados")]
    [SwaggerResponse(200, "Lista de pagos", typeof(ApiResponse<List<Pago>>))]
    public async Task<ActionResult<ApiResponse<List<Pago>>>> ObtenerTodosLosPagos()
    {
        try
        {
            var pagos = await _pagoService.ObtenerTodosLosPagosAsync();
            return Ok(ApiResponse<List<Pago>>.SuccessResult(pagos, $"Se encontraron {pagos.Count} pagos"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<Pago>>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }
}
