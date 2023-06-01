using PolicyOperation.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PolicyOperation.Infrastructure.IoC
{
    public static class DataAccessExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            // ADOs
            services.AddTransient<ICustomRepository, CustomRepository>();
            services.AddTransient<ITimeRepository, TimeRepository>();

            /*
                Context =>  Necesario registrar el repository que adminsitrará la conexión y consultas a la BD desde
                un Context configurado como en ConfigureDatabase en la línea  de este documento.
                Para ver un caso de uso de un repositorio de este estilo ver:
                https://gitlab.gruposancorseguros.com/cross/api-rest-template/-/tree/master/PolicyOperation.Data/Repositories/Context
                
                services.AddTransient(typeof(IContextRepository<>), typeof(ContextRepository<>));
            */
        }
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            /*
                Para utilizar un contexto de Datos es necesario correr un comando de EntityFrameworkCore
                (https://www.thecodebuzz.com/efcore-scaffold-dbcontext-commands-orm-net-core/) que prepara nuestro
                DbContext, configura las relaciones entre tablas y nos brinda clases que representan a las tablas de la BD
                para poder realizar consultas mediante EntityFrameworkCore. 
            
                services.AddDbContext<ClaseContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("nombreConnectionString")),
                    ServiceLifetime.Transient);
            */
        }
    }
}
