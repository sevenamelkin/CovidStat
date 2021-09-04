using System;
using CovidStat.Dto;
using CovidStat.Interfaces;
using CovidStat.Utils;
using Microsoft.Extensions.Logging;

namespace CovidStat.Services
{
    ///<inheritdoc />
    public class MainService : IMainService
    {
        private readonly ILogger<MainService> _logger;
        private readonly IIpService _ipService;
        private readonly ICountryService _countryService;

        public MainService(ILoggerFactory loggerFactory, IIpService ipService, ICountryService countryService)
        {
            _logger = loggerFactory.CreateLogger<MainService>();
            _ipService = ipService;
            _countryService = countryService;
        }

        ///<inheritdoc />
        public Response GetCovidStatByIp(RequestDto requestDto)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetCovidStatByIp)} start");

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
                _logger.LogError(e.Message);
                return new Response
                {
                    ErrorText = e.Message
                };
            }
        }
    }
}