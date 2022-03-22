using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models
{
    /// <summary>
    /// 返回说明
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 调用方
        /// </summary>
        public string CallerName { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginDateTime { get; set; }

        /// <summary>
        /// 调用接口权限
        /// </summary>
        public Dictionary<int, string> Gompetence { get; set; }
    }
}