using System;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using WEB_API.Controllers.U8API.Base;
using WEB_API.Filter;
using WEB_API.Models.U8API;

namespace HzyaMVCWebApiService.Controllers.ApiCO.Interface.ICo
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ICoController : U8ApiBaseController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        internal U8LoginModel InitUserToke([FromBody] JObject json)
        {
            U8LoginModel login = new U8LoginModel();
            login.UserToke = json["cUserToke"].ToString();
            return login;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        internal U8LoginModel InitLogin(U8LoginModel login)
        { 
            //账套号
            if (string.IsNullOrEmpty(login.AccId))
            {
                login.AccId = System.Configuration.ConfigurationManager.AppSettings["pAccId"];
            }
            if (string.IsNullOrEmpty(login.Srv))
            {
                login.Srv = System.Configuration.ConfigurationManager.AppSettings["U8ApiSev"];
            }
            if (string.IsNullOrEmpty(login.Password))
            {
                login.AccId = System.Configuration.ConfigurationManager.AppSettings["pPassword"];
            }
            if (string.IsNullOrEmpty(login.UserId))
            {
                login.AccId = System.Configuration.ConfigurationManager.AppSettings["pUserId"];
            }

            if (string.IsNullOrEmpty(login.Date))
            {
                login.Date = DateTime.Now.ToString("yyyy-MM-dd");
                login.YearId = DateTime.Now.Year.ToString();
            } 
            return login;
        }

        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns></returns>
        /// 
        [ApiExplorerSettings(IgnoreApi = true)]
        internal JObject GetSettingLogin(JObject json)
        {
            string cKey;
            string dKeyDate;
            //因为调取系统的 测试所以添加验证
//#if !DEBUG 
//            //这里在非 DEBUG 模式下编译
//             cKey=json["cKey"]?.ToString();
//             dKeyDate=json["dKeyDate"]?.ToString();
//             if (string.IsNullOrEmpty(cKey) || string.IsNullOrEmpty(dKeyDate))
//             {
//                 throw new Exception("密钥验证未输入");
//            }
//            else
//             if (!ApiCenter.isAllowByKey(cKey, Convert.ToDateTime(dKeyDate)))
//             {
//                 throw new Exception("密钥验证失败");
//             }
//#endif

            string AccId = json["Accid"]?.ToString();
            if (string.IsNullOrEmpty(AccId))
            {
                throw new Exception("账套号未输入 Accid");
            }

            string pDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (json.Property("pDate") !=null)
            {
                pDate = json["pDate"].ToString();
            }

            JObject login = new JObject();
            login.Add("pAccId", AccId);  //数据源@账套号
            login.Add("pYearId", DateTime.Now.Year);
            login.Add("pDate", pDate);
            login.Add("pUserId", System.Configuration.ConfigurationManager.AppSettings["pUserId"]);
            login.Add("pPassword", System.Configuration.ConfigurationManager.AppSettings["pPassword"]);
            login.Add("cSrv", System.Configuration.ConfigurationManager.AppSettings["U8ApiSev"]); 
            return login;
        }


    }
}