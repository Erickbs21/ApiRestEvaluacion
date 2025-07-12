using Microsoft.AspNetCore.Mvc;
using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Request;
using MiBancoAPI.Models.DTOs.Response;
using MiBancoAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Obtiene los datos de un cliente por su DPI
    /// </summary>
    /// <param name="dpi">Número de DPI del cliente (13 dígitos)</param>
    /// <returns>Datos del cliente</returns>
    [HttpGet("{dpi}")]
    [SwaggerOperation(Summary = "Obtener cliente por DPI", Description = "Retorna los datos completos de un cliente específico")]
    [SwaggerResponse(200, "Cliente encontrado", typeof(ApiResponse<Cliente>))]
    [SwaggerResponse(400, "DPI inválido")]
    [SwaggerResponse(404, "Cliente no encontrado")]
    public async Task<ActionResult<ApiResponse<Cliente>>> ObtenerClientePorDPI(
        [Required][StringLength(13, MinimumLength = 13)] string dpi)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Cliente>.ErrorResult("DPI inválido",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
            }

            var cliente = await _clienteService.ObtenerClientePorDPIAsync(dpi);

            if (cliente == null)
            {
                return NotFound(ApiResponse<Cliente>.ErrorResult($"No se encontró un cliente con DPI: {dpi}"));
            }

            return Ok(ApiResponse<Cliente>.SuccessResult(cliente, "Cliente encontrado exitosamente"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Cliente>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }

    /// <summary>
    /// Crea un nuevo cliente
    /// </summary>
    /// <param name="clienteDto">Datos del cliente a crear</param>
    /// <returns>Cliente creado</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Crear nuevo cliente", Description = "Registra un nuevo cliente en el sistema")]
    [SwaggerResponse(201, "Cliente creado exitosamente", typeof(ApiResponse<Cliente>))]
    [SwaggerResponse(400, "Datos inválidos")]
    [SwaggerResponse(409, "Cliente ya existe")]
    public async Task<ActionResult<ApiResponse<Cliente>>> CrearCliente([FromBody] ClienteCreateDto clienteDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Cliente>.ErrorResult("Datos inválidos",
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));
            }

            var nuevoCliente = await _clienteService.CrearClienteAsync(clienteDto);

            return CreatedAtAction(nameof(ObtenerClientePorDPI),
                new { dpi = nuevoCliente.DPI },
                ApiResponse<Cliente>.SuccessResult(nuevoCliente, "Cliente creado exitosamente"));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<Cliente>.ErrorResult(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<Cliente>.ErrorResult(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<Cliente>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }

    /// <summary>
    /// Obtiene todos los clientes activos
    /// </summary>
    /// <returns>Lista de clientes</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obtener todos los clientes", Description = "Retorna la lista de todos los clientes activos")]
    [SwaggerResponse(200, "Lista de clientes", typeof(ApiResponse<List<Cliente>>))]
    public async Task<ActionResult<ApiResponse<List<Cliente>>>> ObtenerTodosLosClientes()
    {
        try
        {
            var clientes = await _clienteService.ObtenerTodosLosClientesAsync();
            return Ok(ApiResponse<List<Cliente>>.SuccessResult(clientes, $"Se encontraron {clientes.Count} clientes"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<Cliente>>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }
}
