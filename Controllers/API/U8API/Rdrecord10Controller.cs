using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Dapper;
using HzyaMVCWebApiService.Controllers.ApiCO.Interface;
using HzyaU8COInterface.CO;
using HzyaU8COInterface.Model;
using Newtonsoft.Json.Linq;
using WEB_API.Filter;
using WEB_API.Models;
using WEB_API.Models.U8API;
using WEB_API.Tools;

namespace WEB_API.Controllers.API.U8API
{
    /// <summary>
    /// 销售出库单
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)]
    public class Rdrecord10Controller : StController
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Rdrecord10Controller() : base(STVoucherType.RdRecord32, "销售出库单")
        {
        }

        /// <summary>
        /// 新增出库单
        /// </summary>
        /// <param name="rdModel">制单参数</param>
        /// <returns></returns> 
        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost]

#if !DEBUG
        [MyAuthorizeAttribute]
#endif
        public ApiReturnModel<object> AddVoucherDispatch([FromBody] DispatchListRd32AddModel dlModel)
        {
            UserModel user = (UserModel)HttpContext.Current.Items["User"];
            ApiReturnModel<object> rj = new ApiReturnModel<object>();

# if DEBUG
            user = new UserModel() { UserName = "测试" };
#endif 
            try
            {
                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", dlModel.ToString(), "新增出库单开始").SQLLog(); //日志输出 

                string iDLsIDs = "";
                for (int i = 0; i < dlModel.BodyList.Count; i++)
                {
                    iDLsIDs += $@"'{dlModel.BodyList[i].iDLsID}',";
                }

                CoVoucherModel<ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>> rdVoucherModel = new CoVoucherModel<ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>>();


                //登录对象补充
                rdVoucherModel.Login = InitLogin(rdVoucherModel.Login);
                Result result = null;
                result = _STCO.Init("ST", rdVoucherModel.Login.AccId, rdVoucherModel.Login.YearId, rdVoucherModel.Login.UserId, rdVoucherModel.Login.Password, rdVoucherModel.Login.Date, rdVoucherModel.Login.Srv);
                if (!result.Success)
                {
                    new ApiLogModel(this.GetType(), ErrorType.警告, user.UserName, "", dlModel.ToString(), "登录失败:" + result.ErrMsgRet).SQLLog();
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                _STCO.StTypeCutover(STVoucherType.RdRecord32);
                _STCO.BeginTrans();


                #region 先入库

                Rdrecord32Model rdrecord10 = null;
                List<Rdrecords32Model> rdrecords10 = null;

                try
                {

                    using (IDbConnection conn = new SqlConnection(GetU8DbConnection))
                    {
                        //{DateTime.Now.Date:yyyy-MM-dd}T00:00:00
                        SqlMapper.GridReader dmModel = conn.QueryMultiple($@" 
select * from (
select 
dl.DLID
,'2022-04-16' ddate
,'0000000001' cCode
,'{user.UserName}' cMaker
,'63' VT_ID 
,'0' bRdFlag
,'10' cVouchType
,'成品入库' cBusType
,'库存' cSource  
,isnull(Warehouse.cWhCode,dls.cWhCode) cWhCode
--,cRdCode
,dl.cDepCode 
,dl.cDefine12
,dl.cDefine2 --是否发货
from DispatchList dl 
join DispatchLists dls on dl.DLID=dls.DLID
left join Customer on Customer.cCusCode=dl.cCusCode
left join Warehouse on Customer.cCusName=Warehouse.cWhName

left join SO_SODetails sode on sode.iSOsID=dls.iSOsID 
left join SO_SOMain so on so.ID=sode.ID
where dls.iDLsID in({iDLsIDs}'')) Rdrecord32Model


select * from (
select  
dls.cInvCode 
--,dls.iQuantity 
,'0' iFlag  
,iDLsID
from DispatchList dl
join DispatchLists dls on dl.DLID=dls.DLID
left join SO_SODetails sode on sode.iSOsID=dls.iSOsID 
left join SO_SOMain so on so.ID=sode.ID
where dls.iDLsID in({iDLsIDs}'') ) Rdrecords32Model
");
                        rdrecord10 = dmModel.Read<Rdrecord32Model>().SingleOrDefault();
                        rdrecords10 = dmModel.Read<Rdrecords32Model>().AsList();
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("查询异常 " + e.Message);
                }




                if (rdrecords10 != null && rdrecords10.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dlModel.CarNumber)) rdrecord10.cDefine3 = dlModel.CarNumber;

                    _STCO.StTypeCutover(STVoucherType.RdRecord10);
                    for (int i = 0; i < dlModel.BodyList.Count; i++)
                    {
                        for (int j = 0; j < rdrecords10.Count; j++)
                        {
                            rdrecords10[j].irowno = j + 1;
                            if (rdrecords10[j].iDLsID == dlModel.BodyList[i].iDLsID)
                            {
                                rdrecords10[j].iQuantity = dlModel.BodyList[i].iQuantity;
                                rdrecords10[j].iDLsID = null;
                            }
                        }
                    }

                    CoVoucherModel<ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>> rdVoucher10Model = new CoVoucherModel<ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>>();
                    rdVoucher10Model.VoucherData = new ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>();
                    rdVoucher10Model.VoucherData.head = rdrecord10;
                    rdVoucher10Model.VoucherData.body = rdrecords10;
                    rj = base.AddVoucherLogin(rdVoucher10Model);


                    new ApiLogModel(this.GetType(), ErrorType.警告, user.UserName, rj.ToString(), dlModel.ToString(), "入库正常返回:").SQLLog(); //日志输出 
                    if (rj.Error != 0)
                    {
                        _STCO.RollbackTrans();
                        return rj;
                    }
                }
                else {
                    rj.Error = -1;
                    rj.Message = "未找到指定出库单行 请校验数据";
                }
                #endregion


                #region 是否需要 出库
                Rdrecord32Model rdrecord32 = null;
                List<Rdrecords32Model> rdrecords32 = null;


                try
                {

                    using (IDbConnection conn = new SqlConnection(GetU8DbConnection))
                    {

                        SqlMapper.GridReader dmModel = conn.QueryMultiple($@"

select * from (
select 
dl.DLID
,'{DateTime.Now.Date:yyyy-MM-dd}T00:00:00' ddate
,'0000000050' cCode
,'{user.UserName}' cMaker
,'87' VT_ID 
,'0' bRdFlag
,'32' cVouchType
,'普通销售' cBusType
,'发货单' cSource
,'0' bTransFlag
,dl.cDLCode cBusCode 
,dls.cWhCode
,cRdCode
,dl.cDepCode
,dl.cPersonCode
,dl.cSTCode
,dl.cCusCode
,dl.cDLCode  
,dl.cDefine12
,dl.cDefine2 --是否发货 
from DispatchList dl 
join DispatchLists dls on dl.DLID=dls.DLID
left join Customer on Customer.cCusCode=dl.cCusCode
left join Warehouse on Customer.cCusName=Warehouse.cWhName
left join SO_SODetails sode on sode.iSOsID=dls.iSOsID 
left join SO_SOMain so on so.ID=sode.ID
where dls.iDLsID in({iDLsIDs}'') and isnull(Warehouse.cWhCode,'')='' ) Rdrecord32Model


select * from (
select  
dls.cInvCode 
--,dls.iQuantity 
,'0' iFlag 
,dls.iDLsID     --发货退货单子表标识
,dls.iSOsID  iorderdid  --订单子表id 
,so.csvouchtype iordertype --订单类型
,dls.cSoCode iordercode --订单号
,sode.iRowNo iorderseq  --销售订单行号 
,'1' isotype    --订单类型 
,dls.cdemandid ipesodid   --pe需求id 
,dls.idemandtype ipesotype  --pe需求类型 
,dls.cdemandcode cpesocode  --pe件需求跟踪号 
,idemandseq ipesoseq   --pe需求行号 
from DispatchList dl
join DispatchLists dls on dl.DLID=dls.DLID
left join Customer on Customer.cCusCode=dl.cCusCode
left join Warehouse on Customer.cCusName=Warehouse.cWhName
left join SO_SODetails sode on sode.iSOsID=dls.iSOsID 
left join SO_SOMain so on so.ID=sode.ID
where dls.iDLsID in({iDLsIDs}'')  and isnull(Warehouse.cWhCode,'')='' ) Rdrecords32Model
");
                        rdrecord32 = dmModel.Read<Rdrecord32Model>().SingleOrDefault();
                        rdrecords32 = dmModel.Read<Rdrecords32Model>().AsList();
                    }

                }
                catch (Exception e)
                {
                    rj.Error = -1;
                    rj.Message = "查询异常" + e.Message;
                    throw new Exception("查询异常 " + e.Message);
                }

                if (rdrecords32 != null && rdrecords32.Count > 0)
                {
                    if (rdrecord32 == null || rdrecords32 == null)
                    {
                        rj.Error = -1;
                        rj.Message = "出库查询异常";
                        throw new Exception("出库查询异常 ");
                    }
                    if (rj.Error == 0 && !string.IsNullOrEmpty(rj.Message))
                    {
                        rdrecord32.cDefine10 = rj.Message;
                    }
                    if (!string.IsNullOrEmpty(dlModel.CarNumber)) rdrecord32.cDefine3 = dlModel.CarNumber;

                    for (int i = 0; i < dlModel.BodyList.Count; i++)
                    {
                        for (int j = 0; j < rdrecords32.Count; j++)
                        {
                            rdrecords32[j].irowno = j + 1;
                            if (rdrecords32[j].iDLsID == dlModel.BodyList[i].iDLsID)
                            {
                                rdrecords32[j].iQuantity = dlModel.BodyList[i].iQuantity;
                            }
                        }
                    }
                    rdVoucherModel.VoucherData = new ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>();
                    rdVoucherModel.VoucherData.head = rdrecord32;
                    rdVoucherModel.VoucherData.body = rdrecords32;
                    rj = base.AddVoucherLogin(rdVoucherModel);
                }
                if (rj.Error != 0)
                {
                    _STCO.RollbackTrans();
                }
                else
                {
                    _STCO.CommitTrans();
                    if(!string.IsNullOrEmpty(rdrecord32?.cDefine10))
                    rj.Message = rdrecord32.cDefine10;
                }
                #endregion


                #region
                using (IDbConnection conn = new SqlConnection(GetU8DbConnection))
                {

                    conn.Execute($@"update DispatchList set cCloser='{user.UserName}' from DispatchList   join DispatchLists  on DispatchList.DLID=DispatchLists.DLID where DispatchLists.iDLsID in({iDLsIDs}'');
                                    update DispatchLists set cSCloser='{user.UserName}' where iDLsID in({iDLsIDs}'');");
                 } 
                #endregion


            }
            catch (Exception e)
            {
                _STCO.RollbackTrans();
                // Log4Helper.Error(this.GetType(), $"{DateTime.Now.ToString("O")}:{_TITLE} Exception:{e.ToString()},data:{rdModel.ToString()} ");
                new ApiLogModel(this.GetType(), ErrorType.警告, user.UserName, "", dlModel.ToString(), "出库单异常:" + e.ToString()).SQLLog(); //日志输出 
                return new ApiReturnModel<object>(-1, e.ToString());
            }
            new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, rj.ToString(), dlModel.ToString(), "方法结束:").SQLLog(); //日志输出 

            return rj;
        }

        /// <summary>
        /// 删除指定单据
        /// </summary>
        /// <param name="cVocCode">入库单号</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost]
#if !DEBUG
        [MyAuthorizeAttribute]
#endif
        public ApiReturnModel<object> DeleteVoucher(string cVocCode)
        {
            ApiReturnModel<object> rj = new ApiReturnModel<object>();
            UserModel user = (UserModel)HttpContext.Current.Items["User"];
#if DEBUG
            user = new UserModel() { UserName = "测试" };
#endif
            try
            {
                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", $"cVocCode:{cVocCode}", "开始删除").SQLLog();

                 
                CoVoucherModel<ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>> rdVoucherModel = new CoVoucherModel<ApiVocModel<Rdrecord32Model, List<Rdrecords32Model>>>();
                //登录对象补充
                rdVoucherModel.Login = InitLogin(rdVoucherModel.Login);
                Result result = null;
                result = _STCO.Init("ST", rdVoucherModel.Login.AccId, rdVoucherModel.Login.YearId, rdVoucherModel.Login.UserId, rdVoucherModel.Login.Password, rdVoucherModel.Login.Date, rdVoucherModel.Login.Srv);
                if (!result.Success)
                {
                    new ApiLogModel(this.GetType(), ErrorType.警告, user.UserName, "", $"cVocCode:{cVocCode}", "登录失败:" + result.ErrMsgRet).SQLLog();
                    return new ApiReturnModel<object>(-1, result.ErrMsgRet);
                }
                _STCO.StTypeCutover(STVoucherType.RdRecord32);
                _STCO.BeginTrans();
                DeleteRd32 rd32;
                try
                {
                    using (IDbConnection conn = new SqlConnection(GetU8DbConnection))
                    {
                        rd32 = conn.Query<DeleteRd32>($@"select rdrecord32.ID rd32ID,rdrecord10.ID rd10ID from rdrecord10   left join rdrecord32 on  rdrecord10.cDefine10=rdrecord32.cCode where rdrecord10.cCode='{cVocCode}'").SingleOrDefault();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("查询异常 " + e.Message);
                }

                if (rd32 == null) { throw new Exception("查询异常 查询结果不存在"); } 

                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, $"cVocCode{cVocCode}", $"rd10ID:{rd32.rd10ID ?? ""}rd32ID:{rd32.rd32ID ?? ""} ", "预计删除单据").SQLLog();

                if (!string.IsNullOrEmpty(rd32?.rd32ID))
                {
                    result = _STCO.DeleteVoucher(rd32.rd32ID);
                }
                if (!result.Success)
                {
                    rj.Error = -2;
                    rj.Message = "出库单删除失败:" + result.ErrMsgRet;
                    _STCO.RollbackTrans();
                }
                else if (!string.IsNullOrEmpty(rd32?.rd10ID))
                {
                    _STCO.StTypeCutover(STVoucherType.RdRecord10);
                    result = _STCO.DeleteVoucher(rd32.rd10ID);
                    if (!result.Success)
                    {
                        rj.Error = -2;
                        rj.Message = "产成品入库单删除失败:" + result.ErrMsgRet;
                        _STCO.RollbackTrans();
                    }
                    else
                    {
                        _STCO.CommitTrans();
                    }
                }
                else
                {
                    _STCO.CommitTrans();
                    rj.Error = 0;
                }
 
                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, $"cVocCode{cVocCode}", $"rj:{rj}", "删除完成").SQLLog();
                return rj;
            }
            catch (Exception e)
            {
                _STCO.RollbackTrans();
                rj.Error = -3;
                rj.Message = "单据删除异常:" + e.Message;
                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, $"cVocCode{cVocCode}", $"rj:{e.ToString()}", "单据删除异常").SQLLog();
                return rj;
            }
        }
    }
}
