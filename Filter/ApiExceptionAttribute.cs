using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace WEB_API.Filter
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常发生
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //记录错误日志
            Task.Run(() =>
            {
                //此处可以调用记录日志方法
            });
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                HttpStatusCode.InternalServerError, new
                {
                    code = HttpStatusCode.InternalServerError,
                    message = actionExecutedContext.Exception.Message
                });
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}