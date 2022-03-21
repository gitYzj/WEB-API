using HzyaMVCWebApiService.Controllers.ApiCO.Interface.ICo;
using HzyaU8COInterface.CO;
using HzyaU8COInterface.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WEB_API.Filter;
using WEB_API.Models;
using WEB_API.Models.U8API;

namespace HzyaMVCWebApiService.Controllers.ApiCO.Interface
{
    /// <summary>
    /// 库存
    /// </summary>
    public class StController : ICoController
    {
        internal STCOInterface _STCO;
        internal string _TITLE = "库存模块";
        internal STVoucherType _vType;

        public StController(STVoucherType vType, string title)
        {
            _STCO = new STCOInterface(vType);
            _vType = vType;
            _TITLE = title;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns> 
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual ApiReturnModel<object> AddVoucherLogin([FromBody] CoVoucherModel<Rdrecord32Model> rdModel)
        {
            try
            {
                //登录对象补充
                rdModel.Login = InitLogin(rdModel.Login); 
                Result result = null;
                result = _STCO.Init("SA", rdModel.Login.AccId, rdModel.Login.YearId, rdModel.Login.UserId, rdModel.Login.Password, rdModel.Login.Date, rdModel.Login.Srv);
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                JObject voucherJson = JObject.FromObject(rdModel);
                voucherJson.Add("domPosition", new JArray());
                result = _STCO.AddNewVoucherJson(voucherJson.ToString());
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                } 
                return new ApiReturnModel<object>(0, $"{result.VouchIdRet}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApiReturnModel<object>(-2, $"{_TITLE}订单制单异常：" + e.Message);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="json">{"UserToke":"","cVoucherId":""}</param>
        /// <returns></returns> 
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual ApiReturnModel<object> GetVoucherLogin([FromBody] JObject json)
        {
            try
            {
                string pAccId = json["login"]["pAccId"].ToString();
                string pYearId = json["login"]["pYearId"].ToString();
                string pUserId = json["login"]["pUserId"].ToString();
                string pPassword = json["login"]["pPassword"].ToString();
                string pDate = json["login"]["pDate"].ToString();
                string cSrv = json["login"]["cSrv"].ToString();
                string cVoucherId = json["cVoucherId"]?.ToString();
                if (string.IsNullOrEmpty(cVoucherId)) return new ApiReturnModel<object>(-1, $"{_TITLE}查询异常：cVoucherId 单据id 未输入");
                Result result = null;
                result = _STCO.Init("SA", pAccId, pYearId, pUserId, pPassword, pDate, cSrv);
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                result = _STCO.LoadVoucherJson($"id='{cVoucherId}'");
                if (!result.Success)
                {
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                return new ApiReturnModel<object>(0,"", JObject.Parse(result.oData as string));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ApiReturnModel<object>(-2, $"{_TITLE}订单制单异常：" + e.Message);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual ApiReturnModel<object> VerifyVoucherLogin([FromBody] JObject json)
        {
            string pAccId = json["login"]["pAccId"].ToString();
            string pYearId = json["login"]["pYearId"].ToString();
            string pUserId = json["login"]["pUserId"].ToString();
            string pPassword = json["login"]["pPassword"].ToString();
            string pDate = json["login"]["pDate"].ToString();
            string cSrv = json["login"]["cSrv"].ToString();
            string cVoucherId = json["cVoucherId"].ToString();
            Result result = null;
            result = _STCO.Init("SA", pAccId, pYearId, pUserId, pPassword, pDate, cSrv);
            if (!result.Success)
            {
                return new ApiReturnModel<object>(-1, result.ErrMsgRet);
            }
            result = _STCO.VerifyVoucher(cVoucherId);
            if (!result.Success)
            {
                return new ApiReturnModel<object>(-1, result.ErrMsgRet);
            }
            return new ApiReturnModel<object>(0, "");
        }
        /// <summary>
        /// 弃审
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual ApiReturnModel<object> UnVerifyVoucherLogin([FromBody] JObject json)
        {
            string pAccId = json["login"]["pAccId"].ToString();
            string pYearId = json["login"]["pYearId"].ToString();
            string pUserId = json["login"]["pUserId"].ToString();
            string pPassword = json["login"]["pPassword"].ToString();
            string pDate = json["login"]["pDate"].ToString();
            string cSrv = json["login"]["cSrv"].ToString();
            string cVoucherId = json["cVoucherId"].ToString();
            Result result = null;
            result = _STCO.Init("SA", pAccId, pYearId, pUserId, pPassword, pDate, cSrv);
            if (!result.Success)
            {
                return new ApiReturnModel<object>(0, result.ErrMsgRet);
            }
            result = _STCO.UnVerifyVoucher(cVoucherId);
            if (!result.Success)
            {
                return new ApiReturnModel<object>(0, result.ErrMsgRet);
            }
            return new ApiReturnModel<object>(1, "");
        }

        /// <summary>
        /// 删除111
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual ApiReturnModel<object> DeleteVoucherLogin([FromBody] JObject json)
        {
            string pAccId = json["login"]["pAccId"].ToString();
            string pYearId = json["login"]["pYearId"].ToString();
            string pUserId = json["login"]["pUserId"].ToString();
            string pPassword = json["login"]["pPassword"].ToString();
            string pDate = json["login"]["pDate"].ToString();
            string cSrv = json["login"]["cSrv"].ToString();
            string cVoucherId = json["cVoucherId"].ToString();
            Result result = null;
            result = _STCO.Init("SA", pAccId, pYearId, pUserId, pPassword, pDate, cSrv);
            if (!result.Success)
            {
                return new ApiReturnModel<object>(0, result.ErrMsgRet);
            }
            result = _STCO.DeleteVoucher(cVoucherId);
            if (!result.Success)
            {
                return new ApiReturnModel<object>(0, result.ErrMsgRet);
            }
            return new ApiReturnModel<object>(1, "");
        }
    }

}