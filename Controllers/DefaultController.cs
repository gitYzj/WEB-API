
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;
using WEB_API.Filter;

namespace WebApplication1.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    public class DefaultController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [MyAuthorize]
        public string About()
        {
            string rtJson = "{\"code\": 0}";
            try
            {

                rtJson = "{\"code\":0,\"data\":[],\"msg\":\"Your application description page.\",\"count\":1}";
            }
            catch
            {
                rtJson = "{\"code\": 0}";
            }
            return rtJson;
        }
         
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}