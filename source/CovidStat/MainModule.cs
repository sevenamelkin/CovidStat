using CovidStat.Interfaces;
using Nancy;
using static CovidStat.Utils.RequestMapper;

namespace CovidStat
{
    /// <summary>
    /// Модуль запросов
    /// </summary>
    public sealed class MainModule : NancyModule
    {
        public MainModule(IMainService mainService)
        {
            Post("/GetCovidStatByIp", x => mainService.GetCovidStatByIp(ToDto(Request)));
        }
    }
}