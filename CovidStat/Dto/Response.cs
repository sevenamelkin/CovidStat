using System.Collections.Generic;

namespace CovidStat.Dto
{
    /// <summary>
    /// Класс ответа сервиса
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Статистика, если всё прошло успешно
        /// </summary>
        public List<CovidStatDto> Statistics { get; set; }
        
        /// <summary>
        /// Текст ошибки, если она случилась
        /// </summary>
        public string ErrorText { get; set; }
    }
}