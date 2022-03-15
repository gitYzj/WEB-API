using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WEB_API.Controllers.API.Base
{
    public class ApiBaseController : ApiController
    {
        internal string GetApiDbConnection
        {
            get { return ConfigurationManager.ConnectionStrings["ApiSystem"].ConnectionString; }
        }
    }
}
