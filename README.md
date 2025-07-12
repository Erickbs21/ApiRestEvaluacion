🚀 MiBanco API: Guía Completa de Pruebas
¡Bienvenido a la guía de pruebas para la API de MiBanco! Este documento te proporcionará todas las herramientas y pasos necesarios para validar a fondo el funcionamiento de nuestra API. Prepárate para explorar, crear y transaccionar con los datos de MiBanco de una manera eficiente y efectiva.

📋 Índice Rápido
⚙️ Configuración Inicial

📮 Pruebas con Postman

💻 Pruebas con cURL

🧪 Casos de Prueba Detallados

🔍 Validaciones Clave

🛠️ Solución de Problemas (Troubleshooting)

📊 Métricas y Datos de Prueba

⚙️ Configuración Inicial
Antes de sumergirnos en las pruebas, asegúrate de tener la API lista y funcionando.

1. Ejecuta la API 🚀
Navega hasta el directorio de la API y arráncala:

Bash

cd MiBancoAPI
dotnet run
2. Verifica que esté Activa ✅
Asegúrate de que la API responde correctamente. Deberías ver una respuesta con información del sistema.

Bash

curl https://localhost:7xxx/api/info
3. Accede a la Documentación de Swagger 📖
Para una visión interactiva de todos los endpoints disponibles, abre tu navegador y visita:

https://localhost:7xxx/index.html

📮 Pruebas con Postman
Postman es tu aliado perfecto para una exploración gráfica e interactiva de la API.

Importa la Colección de Postman 📥
Descarga el archivo MiBanco-API.postman_collection.json.

Abre Postman.

Haz clic en "Import" → selecciona "File" → y elige el archivo descargado.

Configura la variable baseUrl dentro de la colección con el puerto local de tu API (ej. https://localhost:7xxx).

Secuencia de Prueba Recomendada 🧭
Sigue estos pasos para una exploración completa:

1️⃣ Verifica el Estado del Sistema 🟢
GET /api/info

GET /health

2️⃣ Explora Datos Existentes 🕵️‍♀️
GET /api/cliente

GET /api/pago

GET /api/log

3️⃣ Crea un Nuevo Cliente ➕
Envía un POST request a /api/cliente con el siguiente cuerpo:

JSON

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
4️⃣ Realiza Transacciones Clave 💸
Depósito: POST /api/pago

JSON

{
  "dpiCliente": "7777777777777",
  "monto": 1500.00,
  "concepto": "Depósito inicial",
  "tipoPago": 2
}
Retiro: POST /api/pago

JSON

{
  "dpiCliente": "7777777777777",
  "monto": 500.00,
  "concepto": "Retiro cajero",
  "tipoPago": 3
}
5️⃣ Verifica los Resultados 📊
GET /api/cliente/7777777777777

GET /api/pago/cliente/7777777777777

GET /api/log

💻 Pruebas con cURL
Para los amantes de la terminal, cURL te permite interactuar directamente con la API.

Información del Sistema ℹ️
Bash

# Información de la API
curl -X GET "https://localhost:7xxx/api/info" \
  -H "accept: application/json"

# Verificación de salud (Health Check)
curl -X GET "https://localhost:7xxx/health" \
  -H "accept: application/json"
Gestión de Clientes 👤
Bash

# Obtener todos los clientes
curl -X GET "https://localhost:7xxx/api/cliente" \
  -H "accept: application/json"

# Obtener un cliente específico (ej. por DPI)
curl -X GET "https://localhost:7xxx/api/cliente/1234567890101" \
  -H "accept: application/json"

# Crear un nuevo cliente
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
Procesamiento de Pagos 💰
Bash

# Crear un depósito
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

# Crear un retiro
curl -X POST "https://localhost:7xxx/api/pago" \
  -H "Content-Type: application/json" \
  -d '{
    "dpiCliente": "1234567890101",
    "monto": 800.00,
    "concepto": "Retiro en efectivo",
    "tipoPago": 3,
    "numeroReferencia": "RET-2024-001"
  }'

# Realizar un pago de servicios
curl -X POST "https://localhost:7xxx/api/pago" \
  -H "Content-Type: application/json" \
  -d '{
    "dpiCliente": "9876543210987",
    "monto": 350.00,
    "concepto": "Pago de agua potable",
    "tipoPago": 4,
    "numeroReferencia": "EMPAGUA-2024-001"
  }'
Consulta de Logs del Sistema 📜
Bash

# Obtener todos los logs
curl -X GET "https://localhost:7xxx/api/log" \
  -H "accept: application/json"

# Filtrar logs por fecha (ej. 7 de enero de 2024)
curl -X GET "https://localhost:7xxx/api/log/fecha/2024-01-07" \
  -H "accept: application/json"

# Filtrar logs por nivel (1=Info, 2=Warning, 3=Error)
curl -X GET "https://localhost:7xxx/api/log/nivel/1" \
  -H "accept: application/json"
🧪 Casos de Prueba Detallados
Aquí te presentamos ejemplos de payloads para escenarios exitosos y de error.

✅ Casos Exitosos
Cliente Válido
JSON

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
Transacciones Válidas
Depósito:

JSON

{
  "dpiCliente": "1111111111111",
  "monto": 1000.00,
  "concepto": "Depósito mensual",
  "tipoPago": 2
}
Transferencia:

JSON

{
  "dpiCliente": "1111111111111",
  "monto": 750.00,
  "concepto": "Transferencia a familiar",
  "tipoPago": 1,
  "numeroReferencia": "TRF-FAM-001"
}
❌ Casos de Error
Estos payloads deberían generar errores de validación o lógica de negocio.

Cliente Inválido
JSON

{
  "dpi": "123",             // ❌ DPI demasiado corto
  "nombres": "",            // ❌ Nombres vacíos
  "apellidos": "Test123",   // ❌ Apellidos con números
  "email": "email-malo",    // ❌ Formato de email inválido
  "telefono": "123",        // ❌ Teléfono demasiado corto
  "fechaNacimiento": "2020-01-01", // ❌ Cliente menor de edad
  "direccion": "Dir",       // ❌ Dirección demasiado corta
  "saldoInicial": -100      // ❌ Saldo inicial negativo
}
Pago Inválido
JSON

{
  "dpiCliente": "9999999999999", // ❌ Cliente no existente
  "monto": -500.00,             // ❌ Monto negativo
  "concepto": "ABC",            // ❌ Concepto demasiado corto
  "tipoPago": 99                // ❌ Tipo de pago inválido
}
🔍 Validaciones Clave
Comprender las reglas de validación te ayudará a depurar y probar la API de manera efectiva.

Validaciones de Cliente
Campo

Reglas

DPI

13 dígitos, único, solo números

Nombres

2-100 caracteres, solo letras y espacios

Apellidos

2-100 caracteres, solo letras y espacios

Email

Formato válido de email (RFC 5322)

Teléfono

8 dígitos exactos

Edad

Entre 18 y 100 años

Dirección

10-200 caracteres

Saldo Inicial

Entre 0 y 1,000,000


Exportar a Hojas de cálculo
Validaciones de Pago
Campo

Reglas

DPI Cliente

Debe existir en el sistema

Monto

Mayor a 0, máximo 100,000

Concepto

5-200 caracteres

Tipo Pago

1-5 (valores definidos en el enum de la API)

Referencia

Máximo 50 caracteres (opcional)

Notas

Máximo 500 caracteres (opcional)


Exportar a Hojas de cálculo
Lógica de Negocio Importante 🧠
Retiros: Siempre verifican que el saldo sea suficiente antes de procesarse.

Depósitos: Aumentan el saldo del cliente.

Transferencias: Se procesan de forma estándar.

Referencias: Si no se proporcionan, se generan automáticamente.

🛠️ Solución de Problemas (Troubleshooting)
Si encuentras algún obstáculo, aquí hay soluciones a problemas comunes.

Problemas Frecuentes 🚧
1. Error de Certificado SSL (Self-Signed Certificate)
Si usas curl y obtienes un error de certificado, puedes ignorarlo temporalmente (¡solo para desarrollo!):

Bash

# Solución: Agrega -k para ignorar certificados (inseguro en producción)
curl -k -X GET "https://localhost:7xxx/api/info"
2. Puerto Incorrecto 🔌
Asegúrate de usar el puerto correcto. Cuando ejecutas dotnet run, la consola te mostrará el puerto en una línea similar a:

"Now listening on: https://localhost:7xxx"

3. API No Responde 😴
Verifica si la API está ejecutándose:

Bash

dotnet run --verbosity normal
Revisa los logs en la consola o busca archivos de log en la carpeta /logs de tu proyecto.

4. Errores de Validación 🛑
Formato JSON: Confirma que el JSON de tus requests POST y PUT esté bien formado.

Tipos de Datos: Verifica que estés enviando los tipos de datos correctos (números, fechas, cadenas).

Longitudes: Asegúrate de que los campos cumplan con las longitudes mínimas y máximas.

Valores de Enum: Confirma que los valores de los enums (como tipoPago) sean válidos.

Códigos de Estado HTTP Comunes 🚦
Entender los códigos de estado te ayuda a diagnosticar rápidamente.

Código

Significado

Cuándo Ocurre

200

OK

Consultas GET exitosas.

201

Created

Creación de un recurso (POST) exitosa.

400

Bad Request

Datos de entrada inválidos (errores de validación).

404

Not Found

El recurso solicitado no existe.

409

Conflict

Conflicto de datos (ej. DPI ya existe al crear un cliente).

500

Server Error

Un error inesperado en el servidor.


Exportar a Hojas de cálculo
Logs Útiles 📄
Los logs son tus mejores amigos para el diagnóstico.

Bash

# Ver logs en tiempo real (Linux/macOS)
tail -f logs/mibanco-20240107.txt

# Buscar errores específicos
grep "Error" logs/mibanco-*.txt

# Filtrar logs por un cliente específico (ej. por DPI)
grep "1234567890101" logs/mibanco-*.txt
📊 Métricas y Datos de Prueba
Considera estas métricas para evaluar el rendimiento y utiliza los datos predefinidos para tus pruebas.

Rendimiento Esperado ⏱️
Tiempo de respuesta para consultas: Idealmente menos de 200ms.

Tiempo de respuesta para creaciones/transacciones: Idealmente menos de 500ms.

Límite de solicitudes (Rate limit): 100 requests por minuto.

Disponibilidad: Esperamos un 99.9% de tiempo de actividad.

Datos de Prueba Incluidos 📝
Puedes usar estos datos para empezar a interactuar con la API:

Clientes Predefinidos
DPI: 1234567890101 - Juan Carlos García López

DPI: 9876543210987 - María Elena Rodríguez Morales

Pagos Predefinidos
Referencia: REF001 - Pago de servicios básicos (Q500.00)

Referencia: REF002 - Pago de préstamo personal (Q1,200.00)

¡Con esta guía, estás completamente equipado para probar y entender la API de MiBanco! Si tienes alguna pregunta adicional, no dudes en consultar la documentación interactiva en Swagger (https://localhost:7xxx/index.html).

¡Felices pruebas! 🚀🏦
