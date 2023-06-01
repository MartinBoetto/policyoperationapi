namespace PolicyOperation.Data.Entidad.Commands
{
    using Gss.CorporateApps.Data.Ado.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    // -- Modificar nombre respetando el sufijo Command 
    public class SpConParametrosSalidaCommand : CommandResult<bool> // Modificar el tipo de salida del sp según sus necesidades.
    {
        public SpConParametrosSalidaCommand() // configurar parametros
        {

        }
        public override async Task<bool> Execute()
        {
            //var command = DataAccess.CreateSpQuery("NOMBRE_SP")
            //    .SetParameter("NOMBRE_PARAMETRO", VALOR_PARAMETRO)
            //    .SetOutputParameter<int>("NOMBRE_PARAMETRO_SALIDA"); // Especificar tipo 

            //await command.ExecuteCommandAsync();

            //var parametroSalida = command.GetParameter<int>("NOMBRE_PARAMETRO_SALIDA");

            throw new NotImplementedException();
        }
    }
}
