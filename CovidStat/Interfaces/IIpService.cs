using CovidStat.Db.Entities;

namespace CovidStat.Interfaces
{
    /// <summary>
    /// Сервис для получения информации о стране по ip
    /// </summary>
    public interface IIpService
    {
        /// <summary>
        /// Получение названия страны по ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        IpLocation GetCountryByIp(long ip);
    }
}