using System;
using System.Collections.Generic;
using CovidStat.Dto;

namespace CovidStat.Interfaces
{
    /// <summary>
    /// Сервис для получения статистики в стране
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Получить статистику по коду страны
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        List<CovidStatDto> GetStatByCountry(DateTime dateFrom, DateTime dateTo, string countryCode);
    }
}