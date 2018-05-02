using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace PhotoManager.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class ExceptionHandlingAttributeWebApi : ExceptionFilterAttribute
    {
        public string Message { get; set; }

        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();
        public override void OnException(HttpActionExecutedContext context)
        {
            Nlog.Log(LogLevel.Error, context.Exception, RequestToString(context.Request));
            if (context.Exception != null)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Message);
            }
        }

        public static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
            {
                message.Append(request.Method);
            }

            if (request.RequestUri != null)
            {
                message.Append(" ").Append(request.RequestUri);
            }

            return message.ToString();
        }
    }
}