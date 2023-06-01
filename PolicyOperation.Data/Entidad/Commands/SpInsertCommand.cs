namespace PolicyOperation.Data.Entidad.Commands
{
    using Gss.CorporateApps.Data.Ado.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    // -- Modificar nombre respetando el sufijo Command 
    public class SpInsertCommand : Command
    {
        public SpInsertCommand() // configurar parametros
        {

        }
        public override async Task Execute()
        {
            //await DataAccess.CreateSpQuery("NOMBRE_SP")
            //    .SetParameter("NOMBRE_PARAMETRO", VALOR_PARAMETRO)
            //    .ExecuteCommandAsync();

            throw new NotImplementedException();
        }
    }
}
