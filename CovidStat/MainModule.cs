using CovidStat.Dto;
using CovidStat.Interfaces;
using CovidStat.Utils;
using Nancy;

namespace CovidStat
{
    public sealed class MainModule : NancyModule
    {
        private IBaseService _baseService;

        public MainModule(IBaseService baseService)
        {
            _baseService = baseService;
            var requestMapper = new RequestMapper();
            
            Post("/GetCovidStatByIp", x => _baseService.GetCovidStatByIp(requestMapper.ToDto(Request)));
        }
    }
}