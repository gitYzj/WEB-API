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
//    public class PuController : ICoController
//    {
//        private PUCOInterface _PUCO;
//        internal string _TITLE = "U8采购模块";
//        private PuVoucherType typePu;
//        public PuController(PuVoucherType puco, string title)
//        {
//            _PUCO = new PUCOInterface(puco);
//            _TITLE = title;
//            typePu = puco;
//        }

//        /// <summary>
//        /// 新增
//        /// </summary>
//        /// <param name="json"></param>
//        ///  <param name="isToken"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> AddVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
//                Result result = null;
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _PUCO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _PUCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date,
//                        login.Srv);
//                }

//                if (!result.Success) return new ApiReturnModel<object>(0, result.ErrMsgRet);
//                JObject voucherJson = new JObject();
//                voucherJson.Add(new JProperty("head", json["head"]));
//                voucherJson.Add(new JProperty("body", json["body"]));
//                result = _PUCO.AddNewVoucherJson(voucherJson.ToString());
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }

//                return new ApiReturnModel<object>(0, result.VouchIdRet, result.VouchCode);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
//            }
//        }


//        /// <summary>
//        /// 查询
//        /// </summary>
//        /// <param name="json">{"UserToke":"","cVoucherId":""}</param>
//        /// <param name="isToken"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> GetVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
//                Result result = new Result();
//                string cVoucherId = null;

         
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _PUCO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _PUCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date,
//                        login.Srv);
//                }

//                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);

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
//                if (string.IsNullOrEmpty(cVoucherId)) return new ApiReturnModel<object>(-1, "没有订单ID"); ;

//                result = _PUCO.LoadVoucherJson(cVoucherId);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }

//                return new ApiReturnModel<object>(0, JObject.Parse(result.oData as string));
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE} Interface 查询异常：" + e.Message);
//            }
//        }

//        /// <summary>
//        /// 打开
//        /// </summary>
//        /// <param name="json"></param>
//        /// <param name="isToken"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> OpenVoucher([FromBody] JObject json, Boolean isToken)
//        {
//            try
//            {
//                string cVoucherId = json["cVoucherId"].ToString();
//                Result result = null;
//                if (isToken)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _PUCO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _PUCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date,
//                        login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                result = _PUCO.OpenVoucherId(cVoucherId);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                } 
//                return new ApiReturnModel<object>(0,""); 
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
//            }
//        }

//        /// <summary>
//        /// 关闭
//        /// </summary>
//        /// <param name="json"></param>
//        /// <param name="isTokle"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> CloseVoucher([FromBody] JObject json, Boolean isTokle)
//        {
//            try
//            {
//                string cVoucherId = json["cVoucherId"].ToString();
//                Result result = null;
//                if (isTokle)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _PUCO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _PUCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date,
//                        login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                result = _PUCO.CloseVoucherId(cVoucherId);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(0, "");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
//            }
//        }


//        /// <summary>
//        /// 更新
//        /// </summary>
//        /// <param name="json"></param>
//        /// <param name="isTokle"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> UpdateVoucher([FromBody] JObject json, Boolean isTokle)
//        {
//            try
//            { 
//                Result result = null;
//                if (isTokle)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _PUCO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _PUCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date,
//                        login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);


//                #region C0调整时候没有判断,关闭,入库,退库



//                #endregion
                 
//                JObject voucherJson = new JObject();
//                voucherJson.Add(new JProperty("head", json["head"]));
//                voucherJson.Add(new JProperty("body", json["body"])); 
//                result = _PUCO.UpdateVoucherJson(voucherJson.ToString());
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(0, "");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
//            }
//        }
//        //private Result GetVoucharCodeConvertId(string cCode)
//        //{
//        //    Result result = new Result();
//        //    string sql = "";
//        //    switch (typePu)
//        //    {
//        //        case PuVoucherType.采购订单:
//        //            sql = $@"select POID ID from PO_Pomain where cPOID ='{cCode}'";
//        //            break;
//        //        default:
//        //            result.Success = false;
//        //            result.ErrMsgRet = "SaController 订单code 转 ID 暂未支持";
//        //            break;
//        //    }

//        //    Database db = DBFactory.CreateDatabase(DatabaseType.SQL);
//        //    db.SetConnectionString(_PUCO.U8LoginUfDataConnstringForNet);
//        //    ExecuteResult<DataTable> dt = db.ExecuteDataTable(sql);
//        //    if (dt.ExecuteResultType == ExecuteResultType.True)
//        //    {
//        //        if (dt.ReturnObject.Rows.Count == 1)
//        //        {
//        //            result.Success = true;
//        //            result.oData = dt.ReturnObject.Rows[0]["ID"];
//        //        }
//        //        else
//        //        {
//        //            result.Success = false;
//        //            result.ErrMsgRet = $"单据:{_TITLE} 错误:未找到对应单据ID";
//        //        }
//        //    }
//        //    else
//        //    {
//        //        result.Success = false;
//        //        result.ErrMsgRet = dt.ErrorMessage;
//        //    }
//        //    return result;
//        //}


//        /// <summary>
//        /// 审核
//        /// </summary>
//        /// <param name="json"></param>
//        /// <param name="isTokle"></param>
//        /// <returns></returns>
//        public virtual ApiReturnModel<object> VerifyVoucher([FromBody] JObject json, Boolean isTokle)
//        {
//            try
//            {
//                string cVoucherId = json["cVoucherId"].ToString();
//                Result result = null;
//                if (isTokle)
//                {
//                    U8LoginModel login = base.InitUserToke(json);
//                    result = _PUCO.Init(login.UserToke);
//                }
//                else
//                {
//                    U8LoginModel login = base.InitLogin(json);
//                    result = _PUCO.Init("PU", login.AccId, login.YearId, login.UserId, login.Password, login.Date,
//                        login.Srv);
//                }
//                if (!result.Success) return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                result = _PUCO.VerifyVoucherId(cVoucherId);
//                if (!result.Success)
//                {
//                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
//                }
//                return new ApiReturnModel<object>(0, "");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                return new ApiReturnModel<object>(-2, $"{_TITLE}制单异常：" + e.Message);
//            }
//        }
       

//    }
//}