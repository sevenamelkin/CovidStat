using System.IO;
using CovidStat.Dto;
using Nancy;
using Newtonsoft.Json;

namespace CovidStat.Utils
{
    /// <summary>
    /// Маппер сущности запроса <see cref="Request"/> в <see cref="RequestDto"/>
    /// </summary>
    public class RequestMapper
    {
        /// <summary>
        /// Метод преобразования запроса в dto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RequestDto ToDto(Request request)
        {
            using var sr = new StreamReader(request.Body);
            using var reader = new JsonTextReader(sr);
            return new JsonSerializer().Deserialize<RequestDto>(reader);
        }
    }
}