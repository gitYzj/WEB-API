using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WEB_API.Controllers.U8API.Base;
using WEB_API.Filter;
using WEB_API.Models;
using WEB_API.Models.U8API;
using WEB_API.Tools;

namespace WEB_API.Controllers.U8API
{
    /// <summary>
    /// 发货单111111111111111
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)] 
    [MyAuthorizeAttribute]
    public class DispatchListController : U8ApiBaseController
    {
        /// <summary>
        /// 获取发货单列表
        /// </summary>
        /// <param name="dlModel">发货单查询条件</param>
        /// <returns></returns> 
        [HttpPost]
        public ApiReturnModel<List<DispatchListModel>> GetList(DispatchListGetModel dlModel)
        {
            UserModel user = (UserModel)HttpContext.Current.Items["User"];
            try
            {

                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", dlModel.ToString(), "获取发货单列表").SQLLog(); //日志输出

                //Log4Helper.Info(this.GetType(), $"{DateTime.Now.ToString("O")}:{dlModel.ToString()}");
                string sqlWhere = "where 1=1 "; //不关闭的
                if (!string.IsNullOrEmpty(dlModel.iCloser) && dlModel.iCloser == "1") sqlWhere += "and cCloser is not null "; //关闭的
                else
                    sqlWhere += "and cCloser is  null "; //不关闭 
                if (!string.IsNullOrEmpty(dlModel.DLID.ToString())) sqlWhere += $" and dl.DLID='{dlModel.DLID}'";
                if (!string.IsNullOrEmpty(dlModel.cDLCode)) sqlWhere += $" and dl.cDLCode='{dlModel.cDLCode}'";
                if (!string.IsNullOrEmpty(dlModel.dDateStart)) sqlWhere += $" and dDate>='{dlModel.dDateStart}'";
                if (!string.IsNullOrEmpty(dlModel.dDateEnd)) sqlWhere += $" and dDate<='{dlModel.dDateEnd}'";
                if (!string.IsNullOrEmpty(dlModel.dverifysystimeStart)) sqlWhere += $" and dverifysystime>='{dlModel.dverifysystimeStart}'";
                if (!string.IsNullOrEmpty(dlModel.dverifysystimeEnd)) sqlWhere += $" and dverifysystime<='{dlModel.dverifysystimeEnd}'";
                if (!string.IsNullOrEmpty(dlModel.bReturnFlag)) sqlWhere += $" and bReturnFlag='{dlModel.bReturnFlag}'";

                ////单据变动时间
                //string cDefine4Start = json["cDefine4Start"]?.ToString();
                ////单据变动时间
                //string cDefine4End = json["cDefine4End"]?.ToString();

                //if (!string.IsNullOrEmpty(cDefine4Start)) sqlWhere += $" and cDefine4>'{cDefine4Start}'";
                //if (!string.IsNullOrEmpty(cDefine4End)) sqlWhere += $" and cDefine4<='{cDefine4End}'";
                using (IDbConnection conn = new SqlConnection(GetU8DbConnection))
                {
                    List<DispatchListModel> dispatchList = new List<DispatchListModel>();
                    DispatchListModel dispatch = null;
                    IEnumerable<DispatchListModel> dmModel =
                        conn.Query<DispatchListModel, DispatchListsModel, DispatchListModel>($@"
 select 
dl.DLID --发货退货单主表标识
 ,cDLCode --发货退货单号
 ,dDate --单据日期 
 ,dep.cDepCode --部门编码
 ,dep.cDepName --部门名称
 ,cPersonCode --业务员编码
 ,cexch_name --币种名称
 ,iExchRate --汇率 
,cbustype --业务类型
,CONVERT(varchar(100),dcreatesystime, 20) dcreatesystime--制单时间
,CONVERT(varchar(100),dverifysystime , 20) dverifysystime--审核时间  
,bReturnFlag --是否退货 
,CONVERT(varchar(100),cDefine4, 20) cDefine4
,case when ccloser is not null then '关闭' when cverifier is not null then '审核' when cchanger is not null then '变更' else '开立'   end VouchState
,dl.cCusCode
,dl.cCusCode --客户编码
,dl.cRdCode --收发类别
,dl.cSTCode  --销售类型编码 
,dl.cMemo -- 备注  
,dl.cinvoicecompany
,dl.cInvoicecusname

,dls.DLID
,dls.iSOsID
,dls.cSoCode
,dls.idemandtype --需求跟踪方式（1-销售订单行号；4-需求分类号；5-销售订单号）
,dls.iDLsID 
,dls.cWhCode 
,inv.cInvCode 
,inv.cInvName 
,inv.cInvStd 
,dls.iQuantity
,dls.iNum 
,dls.cItemCode
,dls.cItem_class 
,dls.iMoney	
,dls.iTax	
,dls.iSum	
,dls.iDisCount	
,dls.iNatUnitPrice	
,dls.iNatMoney	
,dls.iNatTax	
,dls.iNatSum	
,dls.iNatDisCount
,dls.idemandtype --||订单类型
,dls.iSOsID--||订单子表id 
,dls.cSoCode --||订单号
,dls.iorderrowno --||订单行号 
,dls.cBatch --批号
,dls.cMemo -- 备注 

 from DispatchList dl 
left join Department  dep on dep.cDepCode=dl.cDepCode
left join DispatchLists dls on dls.DLID=dl.DLID
join inventory inv on inv.cInvCode=dls.cInvCode
{sqlWhere}
order by cDefine4,dverifysystime;
            ", (course, location) =>
                        {

                            if (dispatch == null) dispatch = course;
                            if (dispatch.DLID != course.DLID)
                            {
                                dispatchList.Add(dispatch);
                                dispatch = course;
                            }
                            if (location != null)
                                dispatch.body.Add(location);
                            return null;

                            //if (course.body == null)
                            //     course.body = new List<DispatchListsModel>();
                            // course.body.Add(location);
                            // return course;
                        }, splitOn: "DLID");
                    dispatchList.Add(dispatch);

                    new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", dlModel.ToString(), "获取发货单列表完成").SQLLog(); //日志输出 
                    //Log4Helper.Info(this.GetType(), $"{DateTime.Now.ToString("O")}:完成");
                    return new ApiReturnModel<List<DispatchListModel>>(0, "", dispatchList);

                }
            }
            catch (Exception e)
            {
                ;
                new ApiLogModel(this.GetType(), ErrorType.异常, user.UserName, "", dlModel.ToString(), "获取发货单列表异常:" + e.ToString()).SQLLog(); //日志输出 

                // Log4Helper.Error(this.GetType(), $"{DateTime.Now.ToString("O")}:{e.Message}");
                return new ApiReturnModel<List<DispatchListModel>>(-1, e.Message, null); ;
            }


        }
    }
}
