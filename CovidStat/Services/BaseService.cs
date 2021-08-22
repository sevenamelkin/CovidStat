using System.Collections.Generic;
using System.Linq;
using CovidStat.Db.Context;
using CovidStat.Db.Entities;
using CovidStat.Dto;
using CovidStat.Interfaces;
using EFCache.Redis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;

namespace CovidStat.Services
{
    public class BaseService : IBaseService
    {
        private IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly CovidStatDbContext _dbContext;
        private IDatabase _database;

        public BaseService(CovidStatDbContext context, IConfiguration configuration, ILogger logger, IDatabase database)
        {
            _dbContext = context;
            _configuration = configuration;
            _logger = logger;
            _database = database;
        }

        public ResponseDto GetCovidStatByIp(RequestDto requestDto)
        {
            _logger.Information($"{nameof(GetCovidStatByIp)} start");
            
            var ipLocations = _dbContext.IpLocations;

            foreach (var ipLocation in ipLocations)
            {
                _database.SetAdd("ipLocation", JsonConvert.SerializeObject(ipLocation));
            }

            _logger.Information($"input ip: {requestDto.IpAddress}");
            
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