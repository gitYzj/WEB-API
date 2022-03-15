using HzyaMVCWebApiService.Controllers.ApiCO.Interface.ICo;
using HzyaU8COInterface.CO;
using HzyaU8COInterface.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using WEB_API.Filter;
using WEB_API.Models;
using WEB_API.Models.U8API;

namespace HzyaMVCWebApiService.Controllers.ApiCO.Interface
{
    public class APController : ICoController
    {
        private APCOInterface _APCO;
        private string _TITLE = "U8财务模块";

        public APController(APVoucherType apco,string title)
        {
            _APCO = new APCOInterface(apco) ;
            _TITLE = title;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="json"></param>
        /// <param name="isToken"></param>
        /// <returns></returns>
        protected virtual ApiReturnModel<object> AddVoucher([FromBody] JObject json, Boolean isToken = false)
        {
            try
            { 
                Result result = null; 
                if (isToken)
                {
                    U8LoginModel login = base.InitUserToke(json);
                    result = _APCO.Init(login.UserToke);
                }
                else
                {
                    U8LoginModel login = base.InitLogin(json);
                    result = _APCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
                } 
                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet); 
                JObject voucherJson = new JObject();
                voucherJson.Add(new JProperty("head", json["head"]));
                voucherJson.Add(new JProperty("body", json["body"]));
                result = _APCO.AddNewVoucherJson(voucherJson.ToString());
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

        /// 查询
        /// </summary>
        /// <param name="json">{"UserToke":"","cVoucherId":""}</param>
        /// <param name="isToken"></param>
        /// <returns></returns>
        protected virtual ApiReturnModel<object> GetVoucher([FromBody] JObject json, Boolean isToken = false)
        {
            try
            { 
                string cVoucherId = json["cVoucherId"].ToString();
                Result result = null;
                if (isToken)
                {
                    U8LoginModel login = base.InitUserToke(json);
                    result = _APCO.Init(login.UserToke);
                }
                else
                {
                    U8LoginModel login = base.InitLogin(json);
                    result = _APCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
                }
                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                result = _APCO.LoadVoucherJson(cVoucherId);
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                return new ApiReturnModel<object>(0,"", JObject.Parse(result.oData as string));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
            }
        }


    }
}