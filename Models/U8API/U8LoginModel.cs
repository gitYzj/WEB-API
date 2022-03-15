using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models.U8API
{
    public class U8LoginModel
    {
        public string UserToke { get; set; }
        public string AccId { get; set; }
        public string YearId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Date { get; set; }
        public string Srv { get; set; }
    }
}