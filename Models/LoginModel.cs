using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PWD { get; set; }
    }


}