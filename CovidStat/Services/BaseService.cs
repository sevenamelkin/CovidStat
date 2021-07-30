using System;
using CovidStat.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;

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
            _logger.Information("hello!");
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