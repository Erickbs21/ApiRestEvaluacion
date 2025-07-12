using Microsoft.AspNetCore.Mvc;
using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Response;
using MiBancoAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MiBancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

    /// <summary>
    /// Obtiene todos los logs del sistema
    /// </summary>
    /// <returns>Lista de logs</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Obtener todos los logs", Description = "Retorna la bitácora completa del sistema")]
    [SwaggerResponse(200, "Lista de logs", typeof(ApiResponse<List<LogEntry>>))]
    public async Task<ActionResult<ApiResponse<List<LogEntry>>>> ObtenerLogs()
    {
        try
        {
            var logs = await _logService.ObtenerLogsAsync();
            return Ok(ApiResponse<List<LogEntry>>.SuccessResult(logs, $"Se encontraron {logs.Count} registros"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<LogEntry>>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }

    /// <summary>
    /// Obtiene los logs de una fecha específica
    /// </summary>
    /// <param name="fecha">Fecha en formato YYYY-MM-DD</param>
    /// <returns>Lista de logs de la fecha especificada</returns>
    [HttpGet("fecha/{fecha:datetime}")]
    [SwaggerOperation(Summary = "Obtener logs por fecha", Description = "Retorna los logs de una fecha específica")]
    [SwaggerResponse(200, "Lista de logs", typeof(ApiResponse<List<LogEntry>>))]
    public async Task<ActionResult<ApiResponse<List<LogEntry>>>> ObtenerLogsPorFecha(DateTime fecha)
    {
        try
        {
            var logs = await _logService.ObtenerLogsPorFechaAsync(fecha);
            return Ok(ApiResponse<List<LogEntry>>.SuccessResult(logs,
                $"Se encontraron {logs.Count} registros para {fecha:yyyy-MM-dd}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<LogEntry>>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }

    /// <summary>
    /// Obtiene los logs por nivel de severidad
    /// </summary>
    /// <param name="nivel">Nivel de log (Info, Warning, Error, Debug)</param>
    /// <returns>Lista de logs del nivel especificado</returns>
    [HttpGet("nivel/{nivel}")]
    [SwaggerOperation(Summary = "Obtener logs por nivel", Description = "Retorna los logs filtrados por nivel de severidad")]
    [SwaggerResponse(200, "Lista de logs", typeof(ApiResponse<List<LogEntry>>))]
    public async Task<ActionResult<ApiResponse<List<LogEntry>>>> ObtenerLogsPorNivel(LogNivel nivel)
    {
        try
        {
            var logs = await _logService.ObtenerLogsPorNivelAsync(nivel);
            return Ok(ApiResponse<List<LogEntry>>.SuccessResult(logs,
                $"Se encontraron {logs.Count} registros de nivel {nivel}"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<List<LogEntry>>.ErrorResult($"Error interno del servidor: {ex.Message}"));
        }
    }
}
