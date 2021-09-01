using System;

namespace CovidStat.Json
{
    /// <summary>
    /// Класс для десереализации ответа из внешнего запроса
    /// </summary>
    public class CovidStatResponse
    {
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Код страны
        /// </summary>
        public string CountryCode { get; set; }
        
        /// <summary>
        /// Область не используется
        /// </summary>
        public string Province { get; set; }
        
        /// <summary>
        /// Город не используется
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Код города не используется
        /// </summary>
        public string CityCode { get; set; }
        
        /// <summary>
        /// Широта не используется
        /// </summary>
        public string Lat { get; set; }
        
        /// <summary>
        /// Долгота не используется
        /// </summary>
        public string Lon { get; set; }
        
        /// <summary>
        /// Случаев заражения на указанную дату
        /// </summary>
        public int Cases { get; set; }
        
        /// <summary>
        /// Статус не используется
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Дата случаев заражения
        /// </summary>
        public DateTime Date { get; set; }
    }
}