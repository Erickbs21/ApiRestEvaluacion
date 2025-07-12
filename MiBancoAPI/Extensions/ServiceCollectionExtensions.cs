using MiBancoAPI.Services;
using MiBancoAPI.Middleware;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MiBancoAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Servicios de negocio
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IPagoService, PagoService>();
        services.AddScoped<ILogService, LogService>();

        // Filtros
        services.AddScoped<ValidationFilter>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Cache
        services.AddMemoryCache();

        // HTTP Client para servicios externos
        services.AddHttpClient("ExternalBankingService", client =>
        {
            client.BaseAddress = new Uri("https://api.external-bank.com/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MiBanco API",
                Version = "v1",
                Description = "API REST para servicios bancarios de MiBanco - Sistema completo de gestión bancaria",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // Incluir comentarios XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            // Configurar autenticación JWT en Swagger (preparado para futuro)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Habilitar anotaciones de Swagger
            c.EnableAnnotations();

            // Configurar esquemas para enums
            c.SchemaFilter<EnumSchemaFilter>();
        });

        return services;
    }
}

// Filtro personalizado para mejorar la documentación de enums en Swagger
public class EnumSchemaFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
{
    public void Apply(Microsoft.OpenApi.Models.OpenApiSchema schema, Swashbuckle.AspNetCore.SwaggerGen.SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            var enumNames = Enum.GetNames(context.Type);
            var enumValues = Enum.GetValues(context.Type);

            for (int i = 0; i < enumNames.Length; i++)
            {
                schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString($"{enumNames[i]} ({(int)enumValues.GetValue(i)!})"));
            }
        }
    }
}
