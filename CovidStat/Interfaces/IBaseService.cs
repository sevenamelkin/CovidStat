using CovidStat.Dto;

namespace CovidStat.Interfaces
{
    /// <summary>
    /// Сервис обработки запроса
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Метод обработки запроса
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Response GetCovidStatByIp(RequestDto requestDto);
    }
}