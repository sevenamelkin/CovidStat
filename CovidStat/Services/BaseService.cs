using System.Linq;
using CovidStat.Db.Context;
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
        private readonly CovidStatDbContext _dbContext;

        public BaseService(CovidStatDbContext context, IConfiguration configuration, ILogger logger)
        {
            _dbContext = context;
            _configuration = configuration;
            _logger = logger;
        }

        public ResponseDto GetCovidStatByIp(RequestDto requestDto)
        {
            var ipLocations = _dbContext.IpLocations.ToList();

            var numberIpFromRequest = requestDto.IpAddress.ConvertIpToLong();

            var response = ipLocations
                .First(iplocation => iplocation.IpFrom < numberIpFromRequest && iplocation.IpTo > numberIpFromRequest);
            
            return new ResponseDto
            {
                Text = response.CountryName
            };
        }
    }
}