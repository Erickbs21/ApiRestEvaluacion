using MiBancoAPI.Extensions;
using MiBancoAPI.Middleware;
using MiBancoAPI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/mibanco-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Servicios de la aplicación usando extension methods
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// Configurar controladores con validaciones
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Swagger con documentación profesional
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MiBancoPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:3000", "https://mibanco-app.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("ApiPolicy", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 10;
    });
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database")
    .AddCheck<ExternalServiceHealthCheck>("external-services");

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiBanco API v1");
        c.RoutePrefix = "index.html"; // Cambiar de string.Empty a "index.html"
        c.DocumentTitle = "MiBanco API Documentation";
        c.DefaultModelsExpandDepth(-1); // Ocultar modelos por defecto
        c.DisplayRequestDuration();
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("MiBancoPolicy");
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireRateLimiting("ApiPolicy");
app.MapHealthChecks("/health").AllowAnonymous();

// Endpoint de información de la API
app.MapGet("/api/info", () => new
{
    Name = "MiBanco API",
    Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
    Environment = app.Environment.EnvironmentName,
    Timestamp = DateTime.UtcNow,
    Status = "Running",
    Description = "API REST para servicios bancarios - Sistema completo de gestión"
}).WithTags("System").AllowAnonymous();

try
{
    Log.Information("🚀 Iniciando MiBanco API...");
    Log.Information("📊 Swagger disponible en: https://localhost:7xxx/index.html");
    Log.Information("🏥 Health Check disponible en: https://localhost:7xxx/health");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "❌ La aplicación falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
