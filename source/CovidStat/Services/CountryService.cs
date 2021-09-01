using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CovidStat.Dto;
using CovidStat.Interfaces;
using CovidStat.Json;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace CovidStat.Services
{
    ///<inheritdoc />
    public class CountryService : ICountryService
    {
        private readonly ILogger _logger;
        private const string UrlRequest = "https://api.covid19api.com/country/";

        public CountryService(ILogger logger)
        {
            _logger = logger;
        }

        ///<inheritdoc />
        public List<CovidStatDto> GetStatByCountry(DateTime dateFrom, DateTime dateTo, string countryCode)
        {
            _logger.Information($"{nameof(GetStatByCountry)} start, input dateFrom: {dateFrom}, dateTo: {dateTo}, countyCode: {countryCode}");

            var toUtcDateFrom = dateFrom.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var toUtcDateTo = dateTo.ToUniversalTime().ToString(CultureInfo.InvariantCulture);

            var client = new RestClient($"{UrlRequest}{countryCode}/status/confirmed?from={toUtcDateFrom}&to={toUtcDateTo}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (string.IsNullOrEmpty(response.Content))
            {
                throw new Exception($"No response from external service, error text: {response.ErrorMessage}");
            }

            var covidStatResponseFromJson = JsonConvert.DeserializeObject<List<CovidStatResponse>>(response.Content);

            return covidStatResponseFromJson
                .Select(x => new CovidStatDto
                {
                    CountryName = x.Country,
                    Date = x.Date,
                    Cases = x.Cases
                })
                .ToList();
        }
    }
}