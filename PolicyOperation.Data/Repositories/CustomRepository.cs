using System;
using Gss.CorporateApps.Data.Ado.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PolicyOperation.Data.Repositories
{
    public class CustomRepository : AdoRepository, ICustomRepository
    {
        /// <summary>
        /// Constructor de la clase que implementa la Interfaz IAdoRepository genérica que permite realizar consultas y ejecutar
        /// comandos en una base de datos.
        /// </summary>
        /// <param name="config">Servicio de configuración que permite acceder a claves de los archivos de configuración. Lo provee el inyector de dependencias</param>
        /// <param name="httpContextAccesor">Servicio de acceso al contexto HTTP de cada request a la API. Lo provee el inyector de dependencias.
        /// Se utiliza para obtener el <see cref="CancellationToken"/> que viaja en cada request HTTP.
        /// </param>
        public CustomRepository(IConfiguration config, IHttpContextAccessor httpContextAccesor)
            : base(config, httpContextAccesor, database: "DATABASE_NAME", TimeSpan.FromMinutes(1))
        {

        }

    }
}
