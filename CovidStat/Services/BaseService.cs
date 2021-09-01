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
        private readonly IDatabase _database;

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

            var numberIpFromRequest = requestDto.IpAddress.ConvertIpToLong();
            var response = GetCountryByIp(numberIpFromRequest);

            if (response is null)
            {
                return new ResponseDto
                {
                    Text = $"By input ip: {requestDto.IpAddress} county not defined"
                };
            }

            return new ResponseDto
            {
                Text = response.CountryName
            };
        }

        private IpLocation GetCountryByIp(long ip)
        {
            _logger.Information($"{nameof(GetCountryByIp)} start with ip: {ip}");
            var ipString = ip.ToString();
            
            var ipLocationFromCache = _database.StringGet(ipString);

            if (!string.IsNullOrEmpty(ipLocationFromCache))
            {
                _logger.Information($"ip location by ip: {ipString} found in cache, return");
                return JsonConvert.DeserializeObject<IpLocation>(ipLocationFromCache);
            }

            var ipLocationFromEf = _dbContext
                .IpLocations
                .FirstOrDefault(iplocation => iplocation.IpFrom < ip && iplocation.IpTo > ip);

            if (ipLocationFromEf is null || ipLocationFromEf.CountryName.Equals("-"))
            {
                return null;
            }
            
            var successfullyAddCache = _database.StringSet(ip.ToString(), JsonConvert.SerializeObject(ipLocationFromEf));

            if (successfullyAddCache)
            {
                _logger.Information($"ip location by ip: {ipString} found in database and added to cache, return");
            }
            
            return ipLocationFromEf;
        }
    }
}