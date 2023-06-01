using PolicyOperation.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolicyOperation.Models.Entidad;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace PolicyOperation.Infrastructure.IoC
{
    public static class CacheMemory
    {

        public static void RegisterRepositories(IMemoryCache _cacheProvider)
        {
            List<CeiboUserModel> ceiboUserModel = new List<CeiboUserModel>();
            _cacheProvider.Set<List<CeiboUserModel>>("_CeiboUserModel", ceiboUserModel);
        }
        
    }
}
