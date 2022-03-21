using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models.U8API
{
    /// <summary>
    /// 登录对象 (设置了默认值)
    /// </summary>
    public class U8LoginModel
    {
        public string UserToke { get; set; }
        /// <summary>
        /// 账套号
        /// </summary>
        public string AccId { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public string YearId { get; set; }
        /// <summary>
        /// U8用户名
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// U8用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 制单日期 yyyy-MM-dd
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// U8登录地址
        /// </summary>
        public string Srv { get; set; }
    }
}