# 🧪 Guía de Pruebas - MiBanco API

Esta guía te ayudará a probar completamente la API de MiBanco usando diferentes herramientas.

## 📋 Índice

1. [Configuración Inicial](#configuración-inicial)
2. [Pruebas con Postman](#pruebas-con-postman)
3. [Pruebas con cURL](#pruebas-con-curl)
4. [Casos de Prueba](#casos-de-prueba)
5. [Validaciones](#validaciones)
6. [Troubleshooting](#troubleshooting)

---

## ⚙️ Configuración Inicial

### 1. Ejecutar la API
\`\`\`bash
cd MiBancoAPI
dotnet run
\`\`\`

### 2. Verificar que está funcionando
\`\`\`bash
curl https://localhost:7xxx/api/info
\`\`\`

### 3. Acceder a Swagger
Abre tu navegador en: `https://localhost:7xxx/index.html`

---

## 📮 Pruebas con Postman

### Importar Colección

1. **Descargar** el archivo `MiBanco-API.postman_collection.json`
2. **Abrir Postman**
3. **Import** → **File** → Seleccionar el archivo
4. **Configurar variable** `baseUrl` con tu puerto local

### Secuencia Recomendada

#### 1️⃣ Verificar Sistema
\`\`\`
GET /api/info
GET /health
\`\`\`

#### 2️⃣ Explorar Datos Existentes
\`\`\`
GET /api/cliente
GET /api/pago
GET /api/log
\`\`\`

#### 3️⃣ Crear Nuevo Cliente
\`\`\`
POST /api/cliente
{
  "dpi": "7777777777777",
  "nombres": "Carlos Eduardo",
  "apellidos": "Morales Díaz",
  "email": "carlos.morales@email.com",
  "telefono": "77777777",
  "fechaNacimiento": "1988-07-20T00:00:00",
  "direccion": "Zona 12, Ciudad de Guatemala",
  "saldoInicial": 3000.00
}
\`\`\`

#### 4️⃣ Realizar Transacciones
\`\`\`
# Depósito
POST /api/pago
{
  "dpiCliente": "7777777777777",
  "monto": 1500.00,
  "concepto": "Depósito inicial",
  "tipoPago": 2
}

# Retiro
POST /api/pago
{
  "dpiCliente": "7777777777777",
  "monto": 500.00,
  "concepto": "Retiro cajero",
  "tipoPago": 3
}
\`\`\`

#### 5️⃣ Verificar Resultados
\`\`\`
GET /api/cliente/7777777777777
GET /api/pago/cliente/7777777777777
GET /api/log
\`\`\`

---

## 💻 Pruebas con cURL

### Información del Sistema
\`\`\`bash
# API Info
curl -X GET "https://localhost:7xxx/api/info" \
  -H "accept: application/json"

# Health Check
curl -X GET "https://localhost:7xxx/health" \
  -H "accept: application/json"
\`\`\`

### Gestión de Clientes
\`\`\`bash
# Obtener todos los clientes
curl -X GET "https://localhost:7xxx/api/cliente" \
  -H "accept: application/json"

# Obtener cliente específico
curl -X GET "https://localhost:7xxx/api/cliente/1234567890101" \
  -H "accept: application/json"

# Crear nuevo cliente
curl -X POST "https://localhost:7xxx/api/cliente" \
  -H "Content-Type: application/json" \
  -d '{
    "dpi": "8888888888888",
    "nombres": "María José",
    "apellidos": "Hernández Ruiz",
    "email": "maria.hernandez@email.com",
    "telefono": "88888888",
    "fechaNacimiento": "1995-12-10T00:00:00",
    "direccion": "Zona 7, Mixco",
    "saldoInicial": 1800.00
  }'
\`\`\`

### Procesamiento de Pagos
\`\`\`bash
# Crear depósito
curl -X POST "https://localhost:7xxx/api/pago" \
  -H "Content-Type: application/json" \
  -d '{
    "dpiCliente": "1234567890101",
    "monto": 2000.00,
    "concepto": "Depósito por transferencia",
    "tipoPago": 2,
    "numeroReferencia": "DEP-2024-001",
    "notasAdicionales": "Transferencia desde banco externo"
  }'

# Crear retiro
curl -X POST "https://localhost:7xxx/api/pago" \
  -H "Content-Type: application/json" \
  -d '{
    "dpiCliente": "1234567890101",
    "monto": 800.00,
    "concepto": "Retiro en efectivo",
    "tipoPago": 3,
    "numeroReferencia": "RET-2024-001"
  }'

# Pago de servicios
curl -X POST "https://localhost:7xxx/api/pago" \
  -H "Content-Type: application/json" \
  -d '{
    "dpiCliente": "9876543210987",
    "monto": 350.00,
    "concepto": "Pago de agua potable",
    "tipoPago": 4,
    "numeroReferencia": "EMPAGUA-2024-001"
  }'
\`\`\`

### Sistema de Logs
\`\`\`bash
# Obtener todos los logs
curl -X GET "https://localhost:7xxx/api/log" \
  -H "accept: application/json"

# Logs por fecha
curl -X GET "https://localhost:7xxx/api/log/fecha/2024-01-07" \
  -H "accept: application/json"

# Logs por nivel (1=Info, 2=Warning, 3=Error)
curl -X GET "https://localhost:7xxx/api/log/nivel/1" \
  -H "accept: application/json"
\`\`\`

---

## 🧪 Casos de Prueba

### ✅ Casos Exitosos

#### Cliente Válido
\`\`\`json
{
  "dpi": "1111111111111",
  "nombres": "Ana Lucía",
  "apellidos": "Pérez García",
  "email": "ana.perez@gmail.com",
  "telefono": "11111111",
  "fechaNacimiento": "1990-05-15T00:00:00",
  "direccion": "Avenida Las Américas 15-20, Zona 13",
  "saldoInicial": 5000.00
}
\`\`\`

#### Transacciones Válidas
\`\`\`json
// Depósito
{
  "dpiCliente": "1111111111111",
  "monto": 1000.00,
  "concepto": "Depósito mensual",
  "tipoPago": 2
}

// Transferencia
{
  "dpiCliente": "1111111111111",
  "monto": 750.00,
  "concepto": "Transferencia a familiar",
  "tipoPago": 1,
  "numeroReferencia": "TRF-FAM-001"
}
\`\`\`

### ❌ Casos de Error

#### Cliente Inválido
\`\`\`json
{
  "dpi": "123",                    // ❌ Muy corto
  "nombres": "",                   // ❌ Vacío
  "apellidos": "Test123",          // ❌ Contiene números
  "email": "email-malo",           // ❌ Formato inválido
  "telefono": "123",               // ❌ Muy corto
  "fechaNacimiento": "2020-01-01", // ❌ Menor de edad
  "direccion": "Dir",              // ❌ Muy corta
  "saldoInicial": -100             // ❌ Negativo
}
\`\`\`

#### Pago Inválido
\`\`\`json
{
  "dpiCliente": "9999999999999",   // ❌ Cliente no existe
  "monto": -500.00,                // ❌ Monto negativo
  "concepto": "ABC",               // ❌ Muy corto
  "tipoPago": 99                   // ❌ Tipo inválido
}
\`\`\`

---

## 🔍 Validaciones

### Validaciones de Cliente

| Campo | Reglas |
|-------|--------|
| **DPI** | 13 dígitos, único, solo números |
| **Nombres** | 2-100 caracteres, solo letras y espacios |
| **Apellidos** | 2-100 caracteres, solo letras y espacios |
| **Email** | Formato válido de email |
| **Teléfono** | 8 dígitos exactos |
| **Edad** | Entre 18 y 100 años |
| **Dirección** | 10-200 caracteres |
| **Saldo Inicial** | 0 - 1,000,000 |

### Validaciones de Pago

| Campo | Reglas |
|-------|--------|
| **DPI Cliente** | Debe existir en el sistema |
| **Monto** | Mayor a 0, máximo 100,000 |
| **Concepto** | 5-200 caracteres |
| **Tipo Pago** | 1-5 (valores del enum) |
| **Referencia** | Máximo 50 caracteres (opcional) |
| **Notas** | Máximo 500 caracteres (opcional) |

### Lógica de Negocio

- **Retiros**: Verifican saldo suficiente
- **Depósitos**: Aumentan el saldo
- **Transferencias**: Procesan normalmente
- **Referencias**: Se generan automáticamente si no se proporcionan

---

## 🔧 Troubleshooting

### Problemas Comunes

#### 1. Error de Certificado SSL
\`\`\`bash
# Solución: Agregar -k para ignorar certificados
curl -k -X GET "https://localhost:7xxx/api/info"
\`\`\`

#### 2. Puerto Incorrecto
\`\`\`bash
# Verificar el puerto en la consola al ejecutar dotnet run
# Buscar línea similar a: "Now listening on: https://localhost:7xxx"
\`\`\`

#### 3. API No Responde
\`\`\`bash
# Verificar que la API esté ejecutándose
dotnet run --verbosity normal

# Verificar logs en consola
# Verificar archivo de logs en carpeta /logs
\`\`\`

#### 4. Errores de Validación
- **Verificar formato JSON** en requests POST
- **Revisar tipos de datos** (números, fechas, strings)
- **Validar longitudes** de campos
- **Comprobar valores de enums**

### Códigos de Estado HTTP

| Código | Significado | Cuándo Ocurre |
|--------|-------------|---------------|
| **200** | OK | Consultas exitosas |
| **201** | Created | Creación exitosa |
| **400** | Bad Request | Datos inválidos |
| **404** | Not Found | Recurso no encontrado |
| **409** | Conflict | DPI duplicado |
| **500** | Server Error | Error interno |

### Logs Útiles

\`\`\`bash
# Ver logs en tiempo real
tail -f logs/mibanco-20240107.txt

# Buscar errores específicos
grep "Error" logs/mibanco-*.txt

# Filtrar por cliente específico
grep "1234567890101" logs/mibanco-*.txt
\`\`\`

---

## 📊 Métricas de Prueba

### Rendimiento Esperado
- **Tiempo de respuesta**: < 200ms para consultas
- **Tiempo de respuesta**: < 500ms para creaciones
- **Rate limit**: 100 requests/minuto
- **Disponibilidad**: 99.9%

### Datos de Prueba Incluidos

#### Clientes Predefinidos
\`\`\`
DPI: 1234567890101 - Juan Carlos García López
DPI: 9876543210987 - María Elena Rodríguez Morales
\`\`\`

#### Pagos Predefinidos
\`\`\`
REF001 - Pago de servicios básicos (Q500.00)
REF002 - Pago de préstamo personal (Q1,200.00)
\`\`\`

---

¡Con esta guía puedes probar completamente la API de MiBanco! 🚀

Para más información, consulta la documentación en Swagger: `https://localhost:7xxx/index.html`
