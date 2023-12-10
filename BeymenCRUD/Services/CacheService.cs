using Microsoft.Extensions.Caching.Memory;
using BeymenCRUD.Services;
using BeymenCRUD.Data;

namespace BeymenCrud.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly AppDbContext _appDbContext;

        public CacheService(IMemoryCache memoryCache, AppDbContext appDbContext)
        {
            _memoryCache = memoryCache;
            _appDbContext = appDbContext;
        }


        public T GetData<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T item))
            {
                return item;
            }
            else
            {
                T dataFromDatabase = GetDataFromDatabase<T>(key);

                _memoryCache.Set(key, dataFromDatabase, TimeSpan.FromMinutes(1));

                return dataFromDatabase;
            }
        }
        public T GetDataFromDatabase<T>(string key)
        {
            var data = _appDbContext.User.FirstOrDefault(item => item.Id == key);
            return (T)(object)data;
        }

        public object RemoveData(string key)
        {
            var resault = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Remove(key);
                }
                else
                {
                    resault = false;
                }
                return resault;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var resault = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationTime);
                }
                else
                {
                    resault = false;
                }
                return resault;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
