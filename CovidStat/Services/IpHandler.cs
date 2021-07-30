using System.Net;

namespace CovidStat.Services
{
    /// <summary>
    /// Статический обработчик ip адресов
    /// </summary>
    public static class IpHandler 
    {
        /// <summary>
        /// Преобразовать ip-адрес в long-значение
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static long ConvertIpToLong(this string addr)
        {
            return (uint) IPAddress.NetworkToHostOrder(
                (int) IPAddress.Parse(addr).Address);
        }
    }
}