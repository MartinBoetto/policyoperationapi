namespace PolicyOperation.Data.Entidad.Commands
{
    using Gss.CorporateApps.Data.Ado.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    // -- Modificar nombre respetando el sufijo Query 
    public class SpSeleccionQuery : Query<int> // Modificar el tipo de dato de la colección de elementos de salida del sp según sus necesidades.
    {
        public SpSeleccionQuery() // configurar parametros
        {

        }
        public override async Task<IEnumerable<int>> GetResults()
        {
            //var query = await DataAccess.CreateSpQuery("NOMBRE_SP")
            //    .SetParameter("NOMBRE_PARAMETRO", VALOR_PARAMETRO)
            //    .Select(r => new Clase() { 
            //        // Configurar mapeo de columnas del sp a clase.
            //    })
            //    .ToListAsync();
            throw new NotImplementedException();
        }
    }
}
