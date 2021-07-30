using CovidStat.Dto;
using CovidStat.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CovidStat.Services
{
    public class BaseService : IBaseService
    {
        private IConfiguration _configuration;
        private readonly ILogger _logger;

        public BaseService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public ResponseDto GetCovidStatByIp(RequestDto requestDto)
        {
            return new ResponseDto
            {
                Text = requestDto.IpAddress.ConvertIpToLong().ToString()
            };
        }
    }
}