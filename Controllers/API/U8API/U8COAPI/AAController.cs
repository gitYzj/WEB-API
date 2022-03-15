using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http; 
using HzyaMVCWebApiService.Controllers.ApiCO.Interface.ICo;
using HzyaU8COInterface.CO;
using HzyaU8COInterface.Model;
using Newtonsoft.Json.Linq;
using WEB_API.Filter;
using WEB_API.Models;
using WEB_API.Models.U8API;

namespace HzyaMVCWebApiService.Controllers.ApiCO.Interface
{

    public class AAController : ICoController
    {
        private AACOInterface _AACO;
        internal string _TITLE = "基础档案";
        internal string U8LoginUfDataConnstringForNet
        {
            get
            {
                return _AACO.U8LoginUfDataConnstringForNet;
            }
        }

        public AAController(AAType aaType, string title)
        {
            _AACO = new AACOInterface(aaType) ;
            _TITLE = title;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="json"></param>
        /// <param name="isToken"></param>
        /// <returns></returns>
        protected virtual ApiReturnModel<object> AddArchives([FromBody] JObject json, Boolean isToken = false)
        {
            try
            { 
                Result result = null; 
                if (isToken)
                {
                    U8LoginModel login = base.InitUserToke(json);
                    result = _AACO.Init(login.UserToke);
                }
                else
                {
                    U8LoginModel login = base.InitLogin(json);
                    result = _AACO.Init("AA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
                } 
                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);  
                if(json["oArchives"] ==null)
                    return new ApiReturnModel<object>(-1, "没有发现 oArchives 属性");
                result = _AACO.AddArchivesJson(json["oArchives"].ToString());
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                return new ApiReturnModel<object>(0, result.VouchCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="json"></param>
        /// <param name="isToken"></param>
        /// <returns></returns>
        protected virtual ApiReturnModel<object> ModifyArchives([FromBody] JObject json, Boolean isToken = false)
        {
            try
            {
                Result result = null;
                if (isToken)
                {
                    U8LoginModel login = base.InitUserToke(json);
                    result = _AACO.Init(login.UserToke);
                }
                else
                {
                    U8LoginModel login = base.InitLogin(json);
                    result = _AACO.Init("AA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
                }
                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                if (json["oArchives"] == null)
                    return new ApiReturnModel<object>(-1, "没有发现 oArchives 属性");
                result = _AACO.ModifyArchivesJson(json["oArchives"].ToString());
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                return new ApiReturnModel<object>(0, result.VouchCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApiReturnModel<object>(-2, $"{_TITLE}修改异常：" + e.Message);
            }
        }

        ///// <summary>
        ///// 供应商银行添加
        ///// </summary>
        ///// <param name="json"></param>
        ///// <param name="isToken"></param>
        ///// <returns></returns>
        //protected virtual ApiReturnModel<object> AddBanks([FromBody] JObject json, Boolean isToken = false)
        //{
        //    try
        //    {
        //        Result result = null;
        //        if (isToken)
        //        {
        //            U8LoginModel login = base.InitUserToke(json);
        //            result = _AACO.Init(login.UserToke);
        //        }
        //        else
        //        {
        //            U8LoginModel login = base.InitLogin(json);
        //            result = _AACO.Init("AA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
        //        }
        //        if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);

                 
        //        HzyaDBFactory.Database database = (Database)new SqlDatabase(); ;
        //        database.SetConnectionString(U8LoginUfDataConnstringForNet); 
        //        JArray bank = json["Banks"] as JArray;
        //        if (bank != null)
        //        {
        //            string sql = "";
        //            foreach (JObject bankJObject in bank)
        //            { 
        //                string cVenCode = bankJObject["cVenCode"]?.ToString();
        //                cVenCode = string.IsNullOrEmpty(cVenCode) ? "null" : $"'{cVenCode}'";

        //                string cBank = bankJObject["cBank"]?.ToString();
        //                cBank = string.IsNullOrEmpty(cBank) ? "null" : $"'{cBank}'";

        //                string cBranch = bankJObject["cBranch"]?.ToString();
        //                cBranch = string.IsNullOrEmpty(cBranch) ? "null" : $"'{cBranch}'";

        //                string cAccountNum = bankJObject["cAccountNum"]?.ToString();
        //                cAccountNum = string.IsNullOrEmpty(cAccountNum) ? "null" : $"'{cAccountNum}'";

        //                string cAccountName = bankJObject["cAccountName"]?.ToString();
        //                cAccountName = string.IsNullOrEmpty(cAccountName) ? "null" : $"'{cAccountName}'";

        //                string bDefault = bankJObject["bDefault"]?.ToString();
        //                bDefault = string.IsNullOrEmpty(bDefault) ? "null" : $"'{bDefault}'";

        //                sql += $@"insert into VendorBank(cVenCode,cBank,cBranch,cAccountNum,cAccountName,bDefault) values({cVenCode},{cBank},{cBranch},{cAccountNum},{cAccountName},{bDefault});";
        //            }
        //            ExecuteResult<int> enq = database.ExecuteNonQuery(sql);
        //            if (enq.ExecuteResultType != ExecuteResultType.True)
        //            {
        //                return new ApiReturnModel<object>(0, enq.ErrorMessage);  
        //            }
        //            else
        //            {
        //                return new ApiReturnModel<object>(1, "");
        //            }
        //        }
        //        else
        //        {
        //            return new ApiReturnModel<object>(0, $"未检测到参数 Banks：");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return new ApiReturnModel<object>(0, $"{_TITLE}新增银行失败：" + e.Message);
        //    }
        //}
         

        

    }
}