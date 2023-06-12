using Microsoft.Extensions.Caching.Memory;
using PolicyOperation.Data.Repositories;
using PolicyOperation.Data.TimePro;
using PolicyOperation.Models.Entidad;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Core.Managers
{
    public class UserCaching
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IMemoryCache _cacheProvider;
        public UserCaching(ITimeRepository timeRepository, IMemoryCache cacheProvider) 
        { 
            _timeRepository = timeRepository;
            _cacheProvider = cacheProvider;
        }

        public async Task<CeiboUserModel> GetuserFromCache(string token)
            
        {
            string userName = GetUserFromToken(token);

            //busco si tengo el userModel en cache, sino lo guardo
            List<CeiboUserModel> list = _cacheProvider.Get<List<CeiboUserModel>>("_CeiboUserModel");
            CeiboUserModel userModel = list.Find(x => x.UserName == userName); ;

            //si no existe en cache lo busco y grabo
            if (userModel == null)//(!list.Any((x => x.UserName == userName)))
            {
                UserQueryParamsReqModel usqPmodel = new UserQueryParamsReqModel() { UserName = userName };
                var resultQuery = await _timeRepository.QueryAsync(new GetDataUserQuery(usqPmodel));
                userModel = ((IEnumerable<CeiboUserModel>)resultQuery).FirstOrDefault();

                setCeiboUserModelCache(userName, userModel);
            }

            return userModel;
        }



        private string GetUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var sub = tokenS.Claims.First(claim => claim.Type == "nickname").Value;

            string usercode = sub.Substring(sub.IndexOf("|") + 1);
            return usercode;
        }

        private void setCeiboUserModelCache(string userName, CeiboUserModel userModel)
        {

            List<CeiboUserModel> usermodels = _cacheProvider.Get("_CeiboUserModel") as List<CeiboUserModel>;
            usermodels.Add(userModel);
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };
            _cacheProvider.Set("_CeiboUserModel", usermodels, cacheEntryOptions);

        }

    }


   
}
