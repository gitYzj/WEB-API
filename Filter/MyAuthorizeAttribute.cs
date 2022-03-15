using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using WEB_API.Tools;

namespace WEB_API.Filter
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {

        private readonly string TimeStamp = ConfigurationManager.AppSettings["TimeStamp"];

        /// <summary>
        /// 验证入口
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        ///// <summary>
        ///// 验证核心代码
        ///// </summary>
        ///// <param name="httpContext">fbc8ZBLd5ZbtCogcY9NUVV4HZbPln1lb</param>
        ///// <returns></returns>
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{

        //    //前端请求api时会将token存放在名为"auth"的请求头中
        //    var authHeader = httpContext.Request.Headers["auth"];
        //    if (authHeader == null)
        //        return false;

        //    //请求参数
        //    string requestTime = httpContext.Request["rtime"]; //请求时间经过DESC签名
        //    if (string.IsNullOrEmpty(requestTime))
        //        return false;

        //    //模拟生成rtime 时间戳，即登录的时间，加密.　　　　　　　//实际生产中这段代码应该在请求段。此处只为了程序验证通过
        //    string r = DESCryption.Encode(DateTime.Now.ToString());
        //    requestTime = r;


        //    //请求时间RSA解密后加上时间戳的时间即该请求的有效时间
        //    DateTime Requestdt = DateTime.Parse(DESCryption.Decode(requestTime)).AddMinutes(int.Parse(TimeStamp));
        //    DateTime Newdt = DateTime.Now; //服务器接收请求的当前时间

        //    if (Requestdt < Newdt)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (authHeader != null)
        //        {
        //            //进行其他操作
        //            var userinfo = JwtHelp.GetJwtDecode(authHeader);
        //            //举个例子  生成jwtToken 存入redis中    
        //            //这个地方用jwtToken当作key 获取实体val   然后看看jwtToken根据redis是否一样
        //            if (userinfo.UserName == "admin" && userinfo.CallerName == "123")
        //                return true;
        //        }

        //    }

        //    return false;
        //}

        /// <summary>
        /// 验证失败处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("/Error");
            filterContext.HttpContext.Response.Redirect("/Home/Error");
        }


    }
}