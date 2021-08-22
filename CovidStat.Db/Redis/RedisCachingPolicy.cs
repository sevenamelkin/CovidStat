using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using EFCache;

namespace CovidStat.Db.Redis
{
    /// <summary>
    /// Политика кэширования
    /// </summary>
    public class RedisCachingPolicy : CachingPolicy
    {
        protected override void GetExpirationTimeout(ReadOnlyCollection<EntitySetBase> readOnlyCollection, out TimeSpan slidingExpiration, out DateTimeOffset absoluteExpiration)
        {
            slidingExpiration = TimeSpan.FromMinutes(5);
            absoluteExpiration = DateTimeOffset.Now.AddMinutes(30);
        }
    }
}