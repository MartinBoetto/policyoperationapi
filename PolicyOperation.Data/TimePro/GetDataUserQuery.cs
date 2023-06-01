using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gss.CorporateApps.Data;
using Gss.CorporateApps.Data.Ado.Entities;
using PolicyOperation.Data.Repositories;
using PolicyOperation.Models.Entidad;

namespace PolicyOperation.Data.TimePro
{
    public class GetDataUserQuery : Query<CeiboUserModel>
    {
        private readonly int userCode;
        private readonly string username;

        public GetDataUserQuery(UserQueryParamsReqModel model)
        {
            this.userCode = model.UserCode;
            this.username = model.UserName;
        }

        /// <summary>
        /// Devuelve una lista
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<CeiboUserModel>> GetResults()
        {
            string squery = "select personID as BupId, nUserCode as UserCode, sUserName as UserName" +
                " from insudb.extraredusers (nolock) where susername = '" + this.username + "'";
            var querySql = DataAccess.CreateSqlQuery(squery);
            return await querySql.Select(data => new CeiboUserModel()
            {
                BupID = (data.BupId!=null) ? data.BupId : 0,
                UserCode = data.UserCode,
                UserName = data.UserName
            }).ToListAsync();
           
        }

        
    }

}
