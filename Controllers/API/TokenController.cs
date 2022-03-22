using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Mvc;
using Dapper;
using Newtonsoft.Json.Linq;
using WEB_API.Controllers.API.Base;
using WEB_API.Filter;
using WEB_API.Models;
using WEB_API.Tools;

namespace WEB_API.Controllers.API
{ 
    public class TokenController : ApiBaseController
    {
        /// <summary>
        /// Toke获取
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiReturnModel<string> Token(LoginModel user)
        {
            ApiReturnModel<string> result = new ApiReturnModel<string>();

            IEnumerable<dynamic> userList;
            using (IDbConnection conn = new SqlConnection(GetApiDbConnection))
            {
                userList = conn.Query<UserModel>("select * from ApiUser where UserName=@UserName and UserPWd=@UserPWd", user);
            }

            if (userList == null || userList.AsList().Count != 1)
            {
                result.Error = -1;
                result.Message = "验证错误";
                return result;
            }
            else
            {
                UserModel payload = userList.AsList()[0];
                payload.LoginDateTime = DateTime.Now;
                result.Data = JwtHelp.SetJwtEncode(payload);
                result.Error = 0;
                result.Message = "成功";
                using (IDbConnection conn = new SqlConnection(GetApiDbConnection))
                {
                    conn.Execute("update ApiUser set LoginDateTime=@LoginDateTime where UserId=@UserId", payload);
                }
            }
            return result;
        }

    }
}
