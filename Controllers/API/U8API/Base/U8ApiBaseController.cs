using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using WEB_API.Controllers.API.Base;
using WEB_API.Models;

namespace WEB_API.Controllers.U8API.Base
{
    public class U8ApiBaseController : ApiBaseController
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        internal string GetU8DbConnection
        {
            get { return ConfigurationManager.ConnectionStrings["U8db"].ConnectionString; }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        internal virtual Task<ApiReturnModel<string>> GetList([FromBody] JObject json)
        {

            return null;
        }
    }
}