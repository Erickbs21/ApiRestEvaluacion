# 🏦 MiBanco API

**API REST para servicios bancarios** - Sistema completo de gestión bancaria desarrollado en .NET 8

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Status](https://img.shields.io/badge/Status-Active-brightgreen.svg)]()

---

## 📋 Tabla de Contenidos

- [🚀 Inicio Rápido](#-inicio-rápido)
- [📖 Descripción](#-descripción)
- [🏗️ Arquitectura](#️-arquitectura)
- [🔧 Instalación](#-instalación)
- [📚 Documentación de Endpoints](#-documentación-de-endpoints)
- [🧪 Ejemplos de Uso](#-ejemplos-de-uso)
- [⚙️ Configuración](#️-configuración)
- [🔒 Seguridad](#-seguridad)
- [📊 Monitoreo](#-monitoreo)
- [🤝 Contribución](#-contribución)

---

## 🚀 Inicio Rápido

### Prerrequisitos
- .NET 8.0 SDK
- Visual Studio 2022 o VS Code
- Postman (opcional, para pruebas)

### Ejecutar la API
\`\`\`bash
git clone <tu-repositorio>
cd MiBancoAPI
dotnet restore
dotnet run
\`\`\`

### Acceder a la Documentación
- **Swagger UI**: `https://localhost:7xxx/index.html`
- **Health Check**: `https://localhost:7xxx/health`
- **API Info**: `https://localhost:7xxx/api/info`

---

## 📖 Descripción

MiBanco API es un sistema bancario completo que proporciona servicios para:

- ✅ **Gestión de Clientes**: Registro, consulta y administración
- ✅ **Procesamiento de Pagos**: Depósitos, retiros y transferencias
- ✅ **Sistema de Auditoría**: Logging completo de operaciones
- ✅ **Monitoreo**: Health checks y métricas
- ✅ **Documentación**: Swagger UI integrado

### Características Principales

| Característica | Descripción |
|----------------|-------------|
| 🏗️ **Arquitectura Limpia** | Separación de responsabilidades |
| 🔒 **Seguridad** | Rate limiting, CORS, validaciones |
| 📊 **Logging** | Serilog con archivos rotativos |
| 🧪 **Validaciones** | FluentValidation integrado |
| 📚 **Documentación** | Swagger con ejemplos |
| 🏥 **Health Checks** | Monitoreo de servicios |

---

## 🏗️ Arquitectura

\`\`\`
MiBancoAPI/
├── Controllers/          # Controladores de API
├── Models/              # Modelos de datos
│   ├── Entities/        # Entidades del dominio
│   └── DTOs/           # Objetos de transferencia
├── Services/           # Lógica de negocio
├── Middleware/         # Middleware personalizado
├── Extensions/         # Métodos de extensión
├── Validators/         # Validadores FluentValidation
└── Filters/           # Filtros de acción
\`\`\`

### Patrones Implementados
- **Repository Pattern** (simulado en memoria)
- **Dependency Injection**
- **Middleware Pipeline**
- **DTO Pattern**
- **Response Wrapper Pattern**

---

## 🔧 Instalación

### 1. Clonar el Repositorio
\`\`\`bash
git clone <tu-repositorio>
cd MiBancoAPI
\`\`\`

### 2. Restaurar Dependencias
\`\`\`bash
dotnet restore
\`\`\`

### 3. Configurar Appsettings
\`\`\`json
{
  "MiBancoSettings": {
    "MaxTransactionAmount": 100000,
    "MinimumAge": 18,
    "MaxDailyTransactions": 10
  }
}
\`\`\`

### 4. Ejecutar la Aplicación
\`\`\`bash
dotnet run
\`\`\`

---

## 📚 Documentación de Endpoints

### 🏠 **Sistema**

#### GET `/api/info`
Obtiene información general de la API.

**Respuesta:**
\`\`\`json
{
  "name": "MiBanco API",
  "version": "1.0.0",
  "environment": "Development",
  "timestamp": "2024-01-07T10:30:00Z",
  "status": "Running"
}
\`\`\`

#### GET `/health`
Verifica el estado de la API y servicios externos.

**Respuesta:**
\`\`\`json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0123456",
  "entries": {
    "database": {
      "status": "Healthy",
      "description": "Base de datos funcionando correctamente"
    }
  }
}
\`\`\`

---

### 👥 **Clientes**

#### GET `/api/cliente`
Obtiene todos los clientes activos.

**Respuesta:**
\`\`\`json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "dpi": "1234567890101",
      "nombreCompleto": "Juan Carlos García López",
      "email": "juan.garcia@email.com",
      "telefono": "12345678",
      "edad": 38,
      "saldoFormateado": "Q5,000.00",
      "estado": 1,
      "fechaCreacion": "2024-01-07T10:00:00Z"
    }
  ],
  "message": "Se encontraron 2 clientes"
}
\`\`\`

#### GET `/api/cliente/{dpi}`
Obtiene un cliente específico por su DPI.

**Parámetros:**
- `dpi` (string): Número de DPI de 13 dígitos

**Ejemplo:** `GET /api/cliente/1234567890101`

**Respuesta:**
\`\`\`json
{
  "success": true,
  "data": {
    "id": 1,
    "dpi": "1234567890101",
    "nombres": "Juan Carlos",
    "apellidos": "García López",
    "email": "juan.garcia@email.com",
    "telefono": "12345678",
    "fechaNacimiento": "1985-05-15T00:00:00Z",
    "direccion": "Zona 10, Ciudad de Guatemala",
    "saldoCuenta": 5000.00,
    "estado": 1,
    "nombreCompleto": "Juan Carlos García López",
    "edad": 38,
    "saldoFormateado": "Q5,000.00"
  },
  "message": "Cliente encontrado exitosamente"
}
\`\`\`

#### POST `/api/cliente`
Crea un nuevo cliente.

**Body:**
\`\`\`json
{
  "dpi": "5555555555555",
  "nombres": "Ana María",
  "apellidos": "González Pérez",
  "email": "ana.gonzalez@email.com",
  "telefono": "55555555",
  "fechaNacimiento": "1992-03-15T00:00:00",
  "direccion": "Zona 15, Ciudad de Guatemala",
  "saldoInicial": 2500.00
}
\`\`\`

**Validaciones:**
- DPI: 13 dígitos, único
- Nombres/Apellidos: 2-100 caracteres, solo letras
- Email: formato válido
- Teléfono: 8 dígitos
- Edad: mayor de 18 años
- Saldo inicial: 0 - 1,000,000

---

### 💰 **Pagos**

#### GET `/api/pago`
Obtiene todos los pagos registrados.

**Respuesta:**
\`\`\`json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "dpiCliente": "1234567890101",
      "monto": 500.00,
      "montoFormateado": "Q500.00",
      "concepto": "Pago de servicios básicos",
      "tipoPago": 1,
      "numeroReferencia": "REF001",
      "fechaPago": "2024-01-02T10:00:00Z",
      "estado": 3
    }
  ],
  "message": "Se encontraron 2 pagos"
}
\`\`\`

#### GET `/api/pago/cliente/{dpi}`
Obtiene el historial de pagos de un cliente.

**Ejemplo:** `GET /api/pago/cliente/1234567890101`

#### POST `/api/pago`
Crea un nuevo pago.

**Body:**
\`\`\`json
{
  "dpiCliente": "1234567890101",
  "monto": 1000.00,
  "concepto": "Depósito en efectivo",
  "tipoPago": 2,
  "numeroReferencia": "DEP001",
  "notasAdicionales": "Depósito realizado en sucursal"
}
\`\`\`

**Tipos de Pago:**
- `1`: Transferencia
- `2`: Depósito
- `3`: Retiro
- `4`: Pago de Servicios
- `5`: Pago de Préstamo

**Estados de Pago:**
- `1`: Pendiente
- `2`: Procesando
- `3`: Completado
- `4`: Fallido
- `5`: Cancelado

---

### 📋 **Logs**

#### GET `/api/log`
Obtiene todos los logs del sistema.

#### GET `/api/log/fecha/{fecha}`
Obtiene logs de una fecha específica.

**Ejemplo:** `GET /api/log/fecha/2024-01-07`

#### GET `/api/log/nivel/{nivel}`
Obtiene logs por nivel de severidad.

**Niveles:**
- `1`: Info
- `2`: Warning
- `3`: Error
- `4`: Debug

---

## 🧪 Ejemplos de Uso

### Flujo Completo de Operaciones

#### 1. Crear un Cliente
\`\`\`bash
curl -X POST "https://localhost:7xxx/api/cliente" \
  -H "Content-Type: application/json" \
  -d '{
    "dpi": "9999999999999",
    "nombres": "Carlos",
    "apellidos": "Mendoza",
    "email": "carlos@email.com",
    "telefono": "99999999",
    "fechaNacimiento": "1990-01-01T00:00:00",
    "direccion": "Zona 1, Guatemala",
    "saldoInicial": 1000.00
  }'
\`\`\`

#### 2. Realizar un Depósito
\`\`\`bash
curl -X POST "https://localhost:7xxx/api/pago" \
  -H "Content-Type: application/json" \
  -d '{
    "dpiCliente": "9999999999999",
    "monto": 500.00,
    "concepto": "Depósito inicial",
    "tipoPago": 2
  }'
\`\`\`

#### 3. Consultar Saldo
\`\`\`bash
curl -X GET "https://localhost:7xxx/api/cliente/9999999999999"
\`\`\`

#### 4. Ver Historial
\`\`\`bash
curl -X GET "https://localhost:7xxx/api/pago/cliente/9999999999999"
\`\`\`

---

## ⚙️ Configuración

### Variables de Entorno

| Variable | Descripción | Valor por Defecto |
|----------|-------------|-------------------|
| `ASPNETCORE_ENVIRONMENT` | Entorno de ejecución | Development |
| `ASPNETCORE_URLS` | URLs de escucha | https://localhost:7xxx |

### Configuración de Aplicación

\`\`\`json
{
  "MiBancoSettings": {
    "MaxTransactionAmount": 100000,
    "MinimumAge": 18,
    "MaxDailyTransactions": 10,
    "EnableNotifications": true
  },
  "ExternalServices": {
    "BankingServiceUrl": "https://api.external-bank.com",
    "Timeout": "00:00:30"
  }
}
\`\`\`

### Rate Limiting

- **Límite**: 100 requests por minuto
- **Cola**: 10 requests en espera
- **Política**: Ventana fija

---

## 🔒 Seguridad

### Medidas Implementadas

- ✅ **HTTPS Obligatorio**
- ✅ **CORS Configurado**
- ✅ **Rate Limiting**
- ✅ **Validación de Entrada**
- ✅ **Manejo de Excepciones**
- ✅ **Logging de Seguridad**

### Validaciones de Negocio

- **DPI**: Único, 13 dígitos
- **Edad**: Mínimo 18 años
- **Montos**: Límites configurables
- **Saldos**: No negativos
- **Retiros**: Verificación de fondos

---

## 📊 Monitoreo

### Health Checks

La API incluye verificaciones de salud en `/health`:

- **Database**: Simulación de conexión a BD
- **External Services**: Verificación de servicios externos
- **Memory**: Uso de memoria
- **Disk**: Espacio disponible

### Logging

Configurado con **Serilog**:

- **Console**: Desarrollo
- **File**: Archivos rotativos diarios
- **Structured**: Formato JSON
- **Levels**: Info, Warning, Error, Debug

### Métricas

- Tiempo de respuesta por endpoint
- Número de requests por minuto
- Errores por tipo
- Uso de recursos

---

## 🚀 Despliegue

### Desarrollo Local
\`\`\`bash
dotnet run --environment Development
\`\`\`

### Producción
\`\`\`bash
dotnet publish -c Release
dotnet MiBancoAPI.dll --environment Production
\`\`\`

### Docker (Futuro)
\`\`\`dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY . /app
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "MiBancoAPI.dll"]
\`\`\`

---

## 🧪 Testing

### Postman Collection

Importa la colección de Postman incluida para probar todos los endpoints:

1. Abrir Postman
2. Import → File → Seleccionar `MiBanco-API.postman_collection.json`
3. Configurar variable `baseUrl` con tu puerto local

### Casos de Prueba

- ✅ Crear cliente válido
- ✅ Validaciones de DPI
- ✅ Depósitos y retiros
- ✅ Saldos insuficientes
- ✅ Clientes inexistentes
- ✅ Rate limiting
- ✅ Health checks

---

## 📈 Roadmap

### Próximas Características

- [ ] **Autenticación JWT**
- [ ] **Base de Datos Real** (SQL Server/PostgreSQL)
- [ ] **Entity Framework Core**
- [ ] **Unit Tests**
- [ ] **Integration Tests**
- [ ] **Docker Support**
- [ ] **CI/CD Pipeline**
- [ ] **API Versioning**
- [ ] **Caching Redis**
- [ ] **Notifications**

---

## 🤝 Contribución

### Cómo Contribuir

1. Fork el proyecto
2. Crear rama feature (`git checkout -b feature/nueva-caracteristica`)
3. Commit cambios (`git commit -m 'Agregar nueva característica'`)
4. Push a la rama (`git push origin feature/nueva-caracteristica`)
5. Abrir Pull Request

### Estándares de Código

- **C# Conventions**: Microsoft guidelines
- **Naming**: PascalCase para públicos, camelCase para privados
- **Comments**: XML documentation
- **Testing**: Unit tests obligatorios

---

## 📞 Soporte

### Contacto

- **Issues**: GitHub Issues
- **Documentación**: Este README
- **API Docs**: Swagger UI en `/index.html`

### FAQ

**Q: ¿Cómo cambio el puerto?**
A: Modifica `launchSettings.json` o usa `--urls` parameter.

**Q: ¿Dónde están los logs?**
A: En la carpeta `logs/` con rotación diaria.

**Q: ¿Cómo agrego más validaciones?**
A: Extiende los validators en `Validators/`.

---


## 🙏 Agradecimientos

- **Microsoft** por .NET 8
- **Swashbuckle** por Swagger integration
- **Serilog** por logging
- **FluentValidation** por validaciones

---

**¡Gracias por usar MiBanco API!** 🏦✨

*Desarrollado con ❤️ para la evaluacion bancarios modernos*
