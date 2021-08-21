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
            var gg = _dbContext.IpLocations.ToList();
            return new ResponseDto
            {
                Text = requestDto.IpAddress.ConvertIpToLong().ToString()
            };
        }
    }
}