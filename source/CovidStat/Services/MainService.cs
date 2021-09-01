using System;
using CovidStat.Dto;
using CovidStat.Interfaces;
using CovidStat.Utils;
using Serilog;

namespace CovidStat.Services
{
    ///<inheritdoc />
    public class MainService : IMainService
    {
        private readonly ILogger _logger;
        private readonly IIpService _ipService;
        private readonly ICountryService _countryService;

        public MainService(ILogger logger, IIpService ipService, ICountryService countryService)
        {
            _logger = logger;
            _ipService = ipService;
            _countryService = countryService;
        }

        ///<inheritdoc />
        public Response GetCovidStatByIp(RequestDto requestDto)
        {
            try
            {
                _logger.Information($"{nameof(GetCovidStatByIp)} start");

                var numberIpFromRequest = requestDto.Ip.ConvertIpToLong();
                var response = _ipService.GetCountryByIp(numberIpFromRequest);

                if (response is null)
                {
                    throw new Exception($"By input ip: {requestDto.Ip} country not defined");
                }
                
                return new Response
                {
                    Statistics = _countryService.GetStatByCountry(requestDto.DateFrom, requestDto.DateTo, response.CountryCode)
                };
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return new Response
                {
                    ErrorText = e.Message
                };
            }
        }
    }
}