using MiBancoAPI.Models.Entities;
using MiBancoAPI.Models.DTOs.Request;
using System.Collections.Concurrent;

namespace MiBancoAPI.Services;

public class ClienteService : IClienteService
{
    private static readonly ConcurrentBag<Cliente> _clientes = new([
        new Cliente
        {
            Id = 1,
            DPI = "1234567890101",
            Nombres = "Juan Carlos",
            Apellidos = "García López",
            Email = "juan.garcia@email.com",
            Telefono = "12345678",
            FechaNacimiento = new DateTime(1985, 5, 15),
            Direccion = "Zona 10, Ciudad de Guatemala",
            SaldoCuenta = 5000.00m,
            FechaCreacion = DateTime.UtcNow.AddDays(-30)
        },
        new Cliente
        {
            Id = 2,
            DPI = "9876543210987",
            Nombres = "María Elena",
            Apellidos = "Rodríguez Morales",
            Email = "maria.rodriguez@email.com",
            Telefono = "87654321",
            FechaNacimiento = new DateTime(1990, 8, 22),
            Direccion = "Zona 1, Ciudad de Guatemala",
            SaldoCuenta = 3500.50m,
            FechaCreacion = DateTime.UtcNow.AddDays(-15)
        }
    ]);

    private readonly ILogService _logService;

    public ClienteService(ILogService logService)
    {
        _logService = logService;
    }

    public async Task<Cliente?> ObtenerClientePorDPIAsync(string dpi)
    {
        await Task.Delay(10); // Simular operación async

        var cliente = _clientes.FirstOrDefault(c => c.DPI == dpi && c.Activo);

        if (cliente != null)
        {
            await _logService.RegistrarLogAsync("Consulta Cliente",
                $"Se consultó el cliente: {cliente.NombreCompleto} con DPI: {dpi}",
                "GET /api/cliente/{dpi}");
        }
        else
        {
            await _logService.RegistrarLogAsync("Cliente No Encontrado",
                $"No se encontró cliente con DPI: {dpi}",
                "GET /api/cliente/{dpi}", LogNivel.Warning);
        }

        return cliente;
    }

    public async Task<Cliente> CrearClienteAsync(ClienteCreateDto clienteDto)
    {
        await Task.Delay(10); // Simular operación async

        if (await ExisteClientePorDPIAsync(clienteDto.DPI))
        {
            throw new InvalidOperationException($"Ya existe un cliente con DPI: {clienteDto.DPI}");
        }

        // Validar edad mínima
        var edad = DateTime.UtcNow.Year - clienteDto.FechaNacimiento.Year;
        if (edad < 18)
        {
            throw new ArgumentException("El cliente debe ser mayor de edad");
        }

        var nuevoCliente = new Cliente
        {
            Id = _clientes.Count + 1,
            DPI = clienteDto.DPI,
            Nombres = clienteDto.Nombres,
            Apellidos = clienteDto.Apellidos,
            Email = clienteDto.Email,
            Telefono = clienteDto.Telefono,
            FechaNacimiento = clienteDto.FechaNacimiento,
            Direccion = clienteDto.Direccion,
            SaldoCuenta = clienteDto.SaldoInicial,
            FechaCreacion = DateTime.UtcNow,
            Estado = EstadoCliente.Activo
        };

        _clientes.Add(nuevoCliente);

        await _logService.RegistrarLogAsync("Cliente Creado",
            $"Se creó un nuevo cliente: {nuevoCliente.NombreCompleto} con DPI: {nuevoCliente.DPI}, Edad: {nuevoCliente.Edad} años",
            "POST /api/cliente");

        return nuevoCliente;
    }

    public async Task<List<Cliente>> ObtenerTodosLosClientesAsync()
    {
        await Task.Delay(10); // Simular operación async
        return _clientes.Where(c => c.Activo).OrderBy(c => c.Nombres).ToList();
    }

    public async Task<bool> ExisteClientePorDPIAsync(string dpi)
    {
        await Task.Delay(5); // Simular operación async
        return _clientes.Any(c => c.DPI == dpi && c.Activo);
    }

    public async Task<bool> ActualizarSaldoAsync(string dpi, decimal nuevoSaldo)
    {
        await Task.Delay(10); // Simular operación async

        var cliente = _clientes.FirstOrDefault(c => c.DPI == dpi && c.Activo);
        if (cliente == null) return false;

        var saldoAnterior = cliente.SaldoCuenta;
        cliente.SaldoCuenta = nuevoSaldo;

        await _logService.RegistrarLogAsync("Saldo Actualizado",
            $"Cliente {cliente.NombreCompleto}: Saldo anterior Q{saldoAnterior:N2} -> Nuevo saldo Q{nuevoSaldo:N2}",
            "Internal");

        return true;
    }
}
