using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Request;

namespace MiBancoAPI.Services;

public interface IPagoService
{
    Task<Pago> CrearPagoAsync(PagoCreateDto pagoDto);
    Task<List<Pago>> ObtenerPagosPorClienteAsync(string dpi);
    Task<List<Pago>> ObtenerTodosLosPagosAsync();
    Task<decimal> ObtenerTotalPagosPorClienteAsync(string dpi);
}
