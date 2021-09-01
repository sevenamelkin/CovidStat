namespace CovidStat
{
    /// <summary>
    /// Класс констант конфигурации
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Строка подключения к базе данных Postgres
        /// </summary>
        public const string NpgConnectionString = "NpgConnectionString";

        /// <summary>
        /// Строка подключения в базе данных Redis
        /// </summary>
        public const string RedisConnectionString = "RedisConnectionString";

        /// <summary>
        /// Хост сервиса
        /// </summary>
        public const string Host = HttpSection + "Host";

        /// <summary>
        /// Номер порта сервиса
        /// </summary>
        public const string Port = HttpSection + "Port";
        
        /// <summary>
        /// Наименование раздела хоста и порта сервиса
        /// </summary>
        private const string HttpSection = "http:";
    }
}