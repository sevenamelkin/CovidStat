using System;

namespace CovidStat.Dto
{
    public class ErrorResponseDto
    {
        /// <summary>
        /// Тип ошибки
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Сообщение exception
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Cтроковое представление непосредственных кадров в стеке вызова
        /// </summary>
        public string Trace { get; set; }

        /// <summary>
        /// Код состояния ошибки
        /// </summary>
        public int StatusCode { get; set; }
    }
}