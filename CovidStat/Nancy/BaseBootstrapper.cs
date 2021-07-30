// using System;
// using CovidStat.Dto;
// using Nancy;
// using Nancy.Bootstrapper;
// using Nancy.TinyIoc;
//
// namespace CovidStat.Nancy
// {
//     public class BaseBootstrapper : DefaultNancyBootstrapper
//     {
//         protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
//         {
//             pipelines.OnError.AddItemToEndOfPipeline((z, e) =>
//             {
//                 Console.WriteLine($"Error on request {context.Request.Url} : {e.Message}, {e}");
//                 return new ErrorResponseDto
//                 {
//                     Message = e.Message,
//                     Type = e.GetType(),
//                     Trace = e.StackTrace,
//                     StatusCode = (int)HttpStatusCode.InternalServerError
//                 };
//             });
//
//             base.RequestStartup(container, pipelines, context);
//         }
//     }
// }