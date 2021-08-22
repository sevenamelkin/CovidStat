using System.Data.Entity;
using EFCache;
using EFCache.Redis;

namespace CovidStat.Db.Redis
{
    public class Configuration : DbConfiguration
    {
        public Configuration()
        {
            /*//var redisConnection = ConfigurationManager.ConnectionStrings["Redis"].ToString();
            var cache = new RedisCache("hostname:6379,password=");
            var transactionHandler = new CacheTransactionHandler(cache);
            AddInterceptor(transactionHandler);

            Loaded += (sender, args) =>
            {
                args.ReplaceService<BaseService>(
                    (s, _) => new CachingProviderServices(s, transactionHandler, new RedisCachingPolicy())
                );
            };*/
        }
    }
}