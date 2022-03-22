using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace WEB_API.Filter
{
    public class ApiActionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 请求成功之后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                actionExecutedContext.Response.StatusCode, new
                {
                    code = 200,
                    data = JsonConvert.DeserializeObject(actionExecutedContext.Response.Content.ReadAsStringAsync()
                        .Result), //返回给调用者的数据
                    message = "success"
                });
        }

        //public override Task OnActionExecutingAsync(HttpActionContext actionContext,
        //    CancellationToken cancellationToken)
        //{
        //    return base.OnActionExecutingAsync(actionContext, cancellationToken);
        //}

        //public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        //{
        //    return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        //}
    }
}