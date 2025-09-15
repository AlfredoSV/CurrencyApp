using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CurrencyApp.Web.Filters
{
    public class RequestFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            context.HttpContext.Request.EnableBuffering();
            string contollerName = context.Controller.ToString()??string.Empty;
            string actionName = context.ActionDescriptor.DisplayName??string.Empty;
            string method = context.HttpContext.Request.Method;
            string protocol = context.HttpContext.Request.Protocol;
            string host = $"{context.HttpContext.Request.Host.Host}-{context.HttpContext.Request.Host.Port}";
            string path = context.HttpContext.Request.Path;
            string contentType = context.HttpContext.Request.ContentType ?? string.Empty;
            
            string data = context.ActionArguments.Any()
                ? JsonSerializer.Serialize(context.ActionArguments)
                : string.Empty;

            RequestFilterLog requestFilterLog = RequestFilterLog.Create(contollerName, actionName, method, protocol, host, path, contentType, data);

            IRequestService requestService = context.HttpContext.RequestServices.GetService<IRequestService>()!;
            await requestService.SaveRequest(requestFilterLog);

            await next();
        }
    }
}
