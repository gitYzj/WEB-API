using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WEB_API.Models;
using WEB_API.Tools;

namespace WEB_API.Filter
{
    /// <summary>
    /// Token验证
    /// </summary> 
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var httpContext = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase; 
            var authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new
                    ApiReturnModel<object>((int)HttpStatusCode.Unauthorized, "token获取失败了"));
                return;
            } 
            UserModel userinfo = JwtHelp.GetJwtDecode(authHeader);
            if (userinfo == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ApiReturnModel<object>((int)HttpStatusCode.Unauthorized, "token异常") );
                return;
            } 
            if (userinfo.LoginDateTime.AddMinutes(30) < DateTime.Now)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new ApiReturnModel<object>((int)HttpStatusCode.Unauthorized, "token过期了") );
                return;
            } 
            httpContext.Items.Add("User", userinfo); 

        } 
        
    }
}