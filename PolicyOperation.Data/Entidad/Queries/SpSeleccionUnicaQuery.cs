namespace PolicyOperation.Data.Entidad.Queries
{
    using Gss.CorporateApps.Data.Ado.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    // -- Modificar nombre respetando el sufijo Query 
    public class SpSeleccionUnicaQuery : SingleResult<int> // Modificar el tipo de salida del sp según sus necesidades.
    {
        public SpSeleccionUnicaQuery() // configurar parametros
        {

        }
        public override async Task<int> GetResult()
        {
            //var query = await DataAccess.CreateSpQuery("NOMBRE_SP")
            //    .SetParameter("NOMBRE_PARAMETRO", VALOR_PARAMETRO)
            //    .Select(r => new Clase() { 
            //        // Configurar mapeo de columnas del sp a clase.
            //    })
            //    .FirstOrDefaultAsync();

            throw new NotImplementedException();
        }
    }
}
