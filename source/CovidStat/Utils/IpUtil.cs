using System.Net;

namespace CovidStat.Utils
{
    /// <summary>
    /// Статический обработчик ip адресов
    /// </summary>
    public static class IpUtil
    {
        /// <summary>
        /// Преобразовать ip-адрес в long
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