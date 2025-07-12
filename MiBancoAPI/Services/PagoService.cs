using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Request;
using System.Collections.Concurrent;

namespace MiBancoAPI.Services;

public class PagoService : IPagoService
{
    private static readonly ConcurrentBag<Pago> _pagos = new([
        new Pago
        {
            Id = 1,
            DPICliente = "1234567890101",
            Monto = 500.00m,
            Concepto = "Pago de servicios básicos",
            TipoPago = TipoPago.Transferencia,
            NumeroReferencia = "REF001",
            FechaPago = DateTime.UtcNow.AddDays(-5),
            Estado = EstadoPago.Completado
        },
        new Pago
        {
            Id = 2,
            DPICliente = "9876543210987",
            Monto = 1200.00m,
            Concepto = "Pago de préstamo personal",
            TipoPago = TipoPago.Deposito,
            NumeroReferencia = "REF002",
            FechaPago = DateTime.UtcNow.AddDays(-2),
            Estado = EstadoPago.Completado
        }
    ]);

    private readonly IClienteService _clienteService;
    private readonly ILogService _logService;

    public PagoService(IClienteService clienteService, ILogService logService)
    {
        _clienteService = clienteService;
        _logService = logService;
    }

    public async Task<Pago> CrearPagoAsync(PagoCreateDto pagoDto)
    {
        // Validar que el cliente existe
        var cliente = await _clienteService.ObtenerClientePorDPIAsync(pagoDto.DPICliente);
        if (cliente == null)
        {
            throw new InvalidOperationException($"No existe un cliente con DPI: {pagoDto.DPICliente}");
        }

        // Generar número de referencia si no se proporciona
        var numeroReferencia = string.IsNullOrWhiteSpace(pagoDto.NumeroReferencia)
            ? $"REF{DateTime.UtcNow:yyyyMMddHHmmss}"
            : pagoDto.NumeroReferencia;

        var nuevoPago = new Pago
        {
            Id = _pagos.Count + 1,
            DPICliente = pagoDto.DPICliente,
            Monto = pagoDto.Monto,
            Concepto = pagoDto.Concepto,
            TipoPago = pagoDto.TipoPago,
            NumeroReferencia = numeroReferencia,
            FechaPago = DateTime.UtcNow,
            Estado = EstadoPago.Completado
        };

        _pagos.Add(nuevoPago);

        // Actualizar saldo del cliente según el tipo de pago
        switch (pagoDto.TipoPago)
        {
            case TipoPago.Retiro:
                if (cliente.SaldoCuenta >= pagoDto.Monto)
                {
                    await _clienteService.ActualizarSaldoAsync(cliente.DPI, cliente.SaldoCuenta - pagoDto.Monto);
                }
                else
                {
                    nuevoPago.Estado = EstadoPago.Fallido;
                    await _logService.RegistrarLogAsync("Pago Fallido",
                        $"Saldo insuficiente para retiro. Cliente: {cliente.NombreCompleto}, Saldo: {cliente.SaldoFormateado}, Monto solicitado: Q{pagoDto.Monto:N2}",
                        "POST /api/pago", LogNivel.Warning);
                }
                break;

            case TipoPago.Deposito:
                await _clienteService.ActualizarSaldoAsync(cliente.DPI, cliente.SaldoCuenta + pagoDto.Monto);
                break;
        }

        await _logService.RegistrarLogAsync("Pago Creado",
            $"Pago {nuevoPago.Estado}: {nuevoPago.MontoFormateado}, Cliente: {cliente.NombreCompleto}, Concepto: {nuevoPago.Concepto}, Ref: {nuevoPago.NumeroReferencia}",
            "POST /api/pago");

        return nuevoPago;
    }

    public async Task<List<Pago>> ObtenerPagosPorClienteAsync(string dpi)
    {
        await Task.Delay(10); // Simular operación async
        return _pagos.Where(p => p.DPICliente == dpi)
                    .OrderByDescending(p => p.FechaPago)
                    .ToList();
    }

    public async Task<List<Pago>> ObtenerTodosLosPagosAsync()
    {
        await Task.Delay(10); // Simular operación async
        return _pagos.OrderByDescending(p => p.FechaPago).ToList();
    }

    public async Task<decimal> ObtenerTotalPagosPorClienteAsync(string dpi)
    {
        await Task.Delay(10); // Simular operación async
        return _pagos.Where(p => p.DPICliente == dpi && p.Estado == EstadoPago.Completado)
                    .Sum(p => p.Monto);
    }
}
