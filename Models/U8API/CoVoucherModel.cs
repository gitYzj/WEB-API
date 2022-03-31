using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models.U8API
{
    /// <summary>
    /// 新增封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CoVoucherModel<T>
    {
        /// <summary>
        /// 登录信息 (默认不用传)
        /// </summary>
        public U8LoginModel Login { get; set; }
        /// <summary>
        /// 单据信息
        /// </summary>
        public T VoucherData { get; set; }

        public override string ToString()
        {
            return $"{nameof(Login)}: {Login}, {nameof(VoucherData)}: {VoucherData}";
        }
    }
}