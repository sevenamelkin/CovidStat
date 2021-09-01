using CovidStat.Interfaces;
using CovidStat.Utils;
using Nancy;

namespace CovidStat
{
    /// <summary>
    /// Модуль запросов
    /// </summary>
    public sealed class MainModule : NancyModule
    {
        public MainModule(IMainService mainService)
        {
            var requestMapper = new RequestMapper();
            
            Post("/GetCovidStatByIp", x => mainService.GetCovidStatByIp(requestMapper.ToDto(Request)));
        }
    }
}