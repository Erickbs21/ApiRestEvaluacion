using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Request;

namespace MiBancoAPI.Services;

public interface IClienteService
{
    Task<Cliente?> ObtenerClientePorDPIAsync(string dpi);
    Task<Cliente> CrearClienteAsync(ClienteCreateDto clienteDto);
    Task<List<Cliente>> ObtenerTodosLosClientesAsync();
    Task<bool> ExisteClientePorDPIAsync(string dpi);
    Task<bool> ActualizarSaldoAsync(string dpi, decimal nuevoSaldo);
}
