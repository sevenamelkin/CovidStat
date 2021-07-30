using System;
using CovidStat.Interfaces;
using Microsoft.Extensions.Configuration;
using NLog;

namespace CovidStat.Services
{
    public class BaseService : IBaseService
    {
        private IConfiguration _configuration;
        private ILogger _logger;
        
        public BaseService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public AdapterTagValueDto GetStatus()
        {
            var rnd = new Random();
            _logger.Information($"hello! {rnd.Next(0,100)}");
            
            return new AdapterTagValueDto
            {
                Value = _configuration["http:Host"],
                Quality = "awesome virtual",
                Timestamp = DateTime.Now
            };
        }
        
        public class AdapterTagValueDto
        {
            public string Value { get; set; }

            public string Quality { get; set; }

            public DateTime Timestamp { get; set; }
        }
    }
}