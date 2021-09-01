using System;

namespace CovidStat.Dto
{
    /// <summary>
    /// Класс входящего запроса сервиса
    /// </summary>
    public class RequestDto
    {
        /// <summary>
        /// IP Адрес страны
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// С какого периода нужна статистика
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// По какой период
        /// </summary>
        public DateTime DateTo { get; set; }
    }
}