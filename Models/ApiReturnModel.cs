using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace WEB_API.Models
{
    public class ApiReturnModel<T>
    {
        public ApiReturnModel()
        {
        }

        public ApiReturnModel(int error, string message)
        {
            Error = error;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public ApiReturnModel(int error, string message, T data)
        {
            Error = error;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 异常编码 0正常
        /// </summary>
        public int Error { get; set; } = -1;
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>

        public T Data { get; set; }

        public override string ToString()
        {
            return $"{{Error{Error},Error{Error},Message{Message},Data{Data}}}";
        }
    }

    public class ApiVocModel<T,B>
    {
        public T head { get; set; }
        public B body { get; set; }
    }
 
}
 