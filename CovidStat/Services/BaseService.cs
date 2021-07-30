using System;
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

        public Response GetCovidStatByIp(RequestDto requestDto)
        {
            var rnd = new Random();
            _logger.Information($"hello! {rnd.Next(0,100)}");
            
            return new Response
            {
                Text = "ok"
            };
        }
    }
}