using System;

namespace CovidStat.Dto
{
    /// <summary>
    /// Класс для вывода основной информации
    /// </summary>
    public class CovidStatDto
    {
        /// <summary>
        /// Название страны
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Случаев заражения за указанную дату
        /// </summary>
        public int Cases { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
    }
}