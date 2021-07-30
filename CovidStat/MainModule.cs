using CovidStat.Interfaces;
using Nancy;

namespace CovidStat
{
    public sealed class MainModule : NancyModule
    {
        private IBaseService _baseService;
        
        public MainModule(IBaseService baseService)
        {
            _baseService = baseService;
            
            Get("/GetArchive", x =>
            {
                return _baseService.GetStatus();
            });
        }
    }
}