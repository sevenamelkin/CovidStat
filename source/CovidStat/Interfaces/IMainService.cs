using CovidStat.Dto;

namespace CovidStat.Interfaces
{
    /// <summary>
    /// Сервис обработки запроса
    /// </summary>
    public interface IMainService
    {
        /// <summary>
        /// Главный метод обработки запроса
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Response GetCovidStatByIp(RequestDto requestDto);
    }
}