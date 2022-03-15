//using HzyaMVCWebApiService.Controllers.ApiCO.Interface.ICo;
//using HzyaU8COInterface.CO;
//using HzyaU8COInterface.Model;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Data;
//using System.Linq;
//using System.Web.Http;
//using WEB_API.Models;
//using WEB_API.Models.U8API;

//namespace HzyaMVCWebApiService.Controllers.ApiCO.Interface
//{
//    public class SaController : ICoController
//    {
//        private SACOInterface _SACO;
//        internal string _TITLE = "U8销售模块";
//        private SAVoucherTypeSA typeSA;

//        public SaController(SAVoucherTypeSA saco,string title)
//        {
//            _SACO =new SACOInterface(saco) ;
//            typeSA = saco;
//            _TITLE = title;
//        }
         
//        /// <summary>
//        /// 新增
//        /// </summary>
//        /// <param name="json"></param>
//        /// <param name="isToken"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> AddVoucher([FromBody] JObject json,Boolean isToken)
//        {
//            try
//            {
//                Result result;
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _SACO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                JObject voucherJson = new JObject();
//                voucherJson.Add(new JProperty("head", json["head"]));
//                voucherJson.Add(new JProperty("body", json["body"]));
//                result = _SACO.AddNewVoucherJson(voucherJson.ToString());
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(1, result.VouchIdRet);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(0, $"{_TITLE}制单异常：" + e.Message);
//            }
//        }
  
//        /// <summary>
//        /// 查询
//        /// </summary>
//        /// <param name="json">{"UserToke":"","cVoucherId":""}</param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> GetVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
//                Result result = new Result();
//                string cVoucherId=null;
                 
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _SACO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//                }

//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);

//                if (json.Property("cVoucherId") != null)
//                {
//                    cVoucherId = json["cVoucherId"].ToString();
//                }
//                if (string.IsNullOrEmpty(cVoucherId) && json.Property("cVoucherCode") != null)
//                {
//                    result = GetVoucharCodeConvertId(json["cVoucherCode"].ToString());
//                    if (result.Success) cVoucherId = result.oData.ToString();
//                    else
//                        return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                if (string.IsNullOrEmpty(cVoucherId))
//                    return new ApiReturnModel<object>(-1, "没有订单ID");
                 
//                result = _SACO.LoadVoucherJson(cVoucherId);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(0, "",JObject.Parse(result.oData as string));
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE} Interface 查询异常：" + e.Message);
//            }
//        }

//        /// <summary>
//        /// 审核
//        /// </summary>
//        /// <param name="json">{"UserToke":"","cVoucherId":""}</param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> VerifyVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
//                Result result = new Result();
//                string cVoucherId = null; 
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _SACO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//                } 
//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet); 
//                if (json.Property("cVoucherId") != null)
//                {
//                    cVoucherId = json["cVoucherId"].ToString();
//                }
//                if (string.IsNullOrEmpty(cVoucherId) && json.Property("cVoucherCode") != null)
//                {
//                    result = GetVoucharCodeConvertId(json["cVoucherCode"].ToString());
//                    if (result.Success) cVoucherId = result.oData.ToString();
//                    else
//                        return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                if (string.IsNullOrEmpty(cVoucherId))
//                    return new ApiReturnModel<object>(-1, "没有订单ID"); 
//                result = _SACO.VerifyVoucherId(cVoucherId, true);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(0, "",JObject.Parse(result.oData as string));
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE} Interface 查询异常：" + e.Message);
//            }
//        }

//        /// <summary>
//        /// 弃审
//        /// </summary>
//        /// <param name="json">{"UserToke":"","cVoucherId":""}</param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> UnVerifyVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
//                Result result = new Result();
//                string cVoucherId = null;
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _SACO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                if (json.Property("cVoucherId") != null)
//                {
//                    cVoucherId = json["cVoucherId"].ToString();
//                }
//                if (string.IsNullOrEmpty(cVoucherId) && json.Property("cVoucherCode") != null)
//                {
//                    result = GetVoucharCodeConvertId(json["cVoucherCode"].ToString());
//                    if (result.Success) cVoucherId = result.oData.ToString();
//                    else
//                        return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                if (string.IsNullOrEmpty(cVoucherId))
//                    return new ApiReturnModel<object>(-1, "没有订单ID");
//                result = _SACO.VerifyVoucherId(cVoucherId, false);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(0, "",JObject.Parse(result.oData as string));
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE} Interface 查询异常：" + e.Message);
//            }
//        }
//        ///// <summary>
//        ///// 删除
//        ///// </summary>
//        ///// <param name="json"></param>
//        ///// <param name="isToken"></param>
//        ///// <returns></returns>
//        //public virtual ApiReturnModel<object> DeleteVoucher([FromBody] JObject json, Boolean isToken)
//        //{
//        //    try
//        //    {

//        //        Result result = null;
//        //        if (isToken)
//        //        {
//        //            U8LoginModel login = base.InitUserToke(json);
//        //            result = _SACO.Init(login.UserToke);
//        //        }
//        //        else
//        //        {
//        //            U8LoginModel login = base.InitLogin(json);
//        //            result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//        //        }
//        //        if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);
//        //        JObject voucherJson = new JObject();
//        //        voucherJson.Add(new JProperty("head", json["head"]));
//        //        voucherJson.Add(new JProperty("body", json["body"]));
//        //        result = _SACO.
//        //        if (!result.Success)
//        //        {
//        //            return new ApiReturnModel<object>(0, result.ErrMsgRet);
//        //        }
//        //        return new ApiReturnModel<object>(1, "");
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine(e);
//        //        return new ApiReturnModel<object>(0, $"{_TITLE}订单制单异常：" + e.Message);
//        //    }
//        //}

//        public virtual ApiReturnModel<object> UpdateVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
 
//                Result result = null;
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _SACO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                JObject voucherJson = new JObject();
//                voucherJson.Add(new JProperty("head", json["head"]));
//                voucherJson.Add(new JProperty("body", json["body"]));
//                result = _SACO.UpdateVoucherJson(voucherJson.ToString()); 
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(1, "");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(0, $"{_TITLE}订单制单异常：" + e.Message);
//            }
//        }
          
//        public virtual ApiReturnModel<object> ChangeVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            { 
//                Result result = null;
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _SACO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _SACO.Init("SA", login.AccId, login.YearId, login.UserId, login.Password, login.Date, login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                JObject voucherJson = new JObject();
//                voucherJson.Add(new JProperty("head", json["head"]));
//                voucherJson.Add(new JProperty("body", json["body"]));
//                result = _SACO.ChangeVoucherJson(voucherJson.ToString());
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(1,"");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(0, $"{_TITLE}订单制单异常：" + e.Message);
//            }
//        }

//        private Result GetVoucharCodeConvertId(string cCode)
//        {
//            Result result = new Result();
//            string sql = "";
//            switch (typeSA)
//            {
//                case SAVoucherTypeSA.SODetails:
//                    sql = $@"select ID from so_somain where csocode='{cCode}'";
//                    break;
//                default:
//                    result.Success = false;
//                    result.ErrMsgRet = $"单据:{_TITLE} 错误:code 转 ID 暂未支持";
//                    break; 
//            } 

//            Database db = DBFactory.CreateDatabase(DatabaseType.SQL);
//            db.SetConnectionString(_SACO.U8LoginUfDataConnstringForNet);
//            ExecuteResult<DataTable> dt= db.ExecuteDataTable(sql);
//            if (dt.ExecuteResultType== ExecuteResultType.True)
//            {
//                if (dt.ReturnObject.Rows.Count==1)
//                { 
//                    result.Success = true;
//                    result.oData = dt.ReturnObject.Rows[0]["ID"];
//                }
//                else
//                {
//                    result.Success = false;
//                    result.ErrMsgRet = $"单据:{_TITLE} 错误:未找到对应单据ID"; 
//                }  
//            }
//            else
//            {
//                result.Success = false;
//                result.ErrMsgRet = dt.ErrorMessage; 
//            } 
//            return result;
//        }
//    }
//}