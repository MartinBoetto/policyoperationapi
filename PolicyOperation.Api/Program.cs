using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolicyOperation.Infrastructure.IoC;
using Gss.CorporateApps.Infrastructure;
using Gss.CorporateApps.Infrastructure.IoC;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using Serilog;
using PolicyOperation.Infrastructure.AppConfiguration;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Registra un servicio que permite acceder a todo el contexto HTTP.
builder.Services.AddHttpContextAccessor();
// Agregar archivos de configuración.
builder.ApplyConfigurationFiles((context, appConfiguration) =>
{
    appConfiguration.AppSettingsFilePath = $"Config/appsettings.{context.HostingEnvironment.EnvironmentName}.json";
    appConfiguration.SecureSettingsFilePath = $"Config/secureSettings.{context.HostingEnvironment.EnvironmentName}.json";
    appConfiguration.ConnectionStringsSettingsFilePath = $"Config/connectionStrings.{context.HostingEnvironment.EnvironmentName}.json";
    appConfiguration.ExternalServicesSettingsFilePath = $"Config/externalServices.{context.HostingEnvironment.EnvironmentName}.json";
});

// Primero preparar instancia que me permita acceder a las propiedades de configuración de la App.
builder.Services.BindAppSettings(builder.Configuration);

// Configurar App GSS.
builder.UseGssApp((context, appConfiguration) =>
{
    appConfiguration.UseHandlersRequestsAndValidators = true;
    appConfiguration.AssemblyNameWithHandlers = "PolicyOperation.Core";
    appConfiguration.LoggingConfiguration = new LoggingConfiguration()
    {
        PathToLogFile = AppConfig.Logging.PathUrl,
        LogEventLevel = LogEventLevel.Information,
        LogLevel = LogLevel.Information,
        RollingInterval = RollingInterval.Day
    };
    appConfiguration.AddEnvironmentVariables = true;
});

// Registrar los repositorios y external services como servicios dentro del inyector de dependencias.
builder.Services.RegisterRepositories();
builder.Services.RegisterExternalServices();

//Registro la MemoryCache
//CacheMemory.RegisterRepositories(IMemoryCache _memoriCache);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.EnableAnnotations());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

