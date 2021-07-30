using System.IO;
using CovidStat.Dto;
using Nancy;
using Newtonsoft.Json;

namespace CovidStat
{
    /// <summary>
    /// Маппер сущности запроса <see cref="Request"/> в <see cref="AdapterGetValueRequest"/>
    /// </summary>
    public class RequestMapper
    {
        public RequestDto ToDto(Request request)
        {
            using (var sr = new StreamReader(request.Body))
            using (var reader = new JsonTextReader(sr))
            {
                return new JsonSerializer().Deserialize<RequestDto>(reader);
            }
        }
    }
}