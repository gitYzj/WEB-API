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
    public class Rdrecord32Controller : StController
    { 
        /// <summary>
        /// 初始化
        /// </summary>
        public Rdrecord32Controller() : base(STVoucherType.RdRecord32, "销售出库单")
        {
        }

        /// <summary>
        /// 新增出库单
        /// </summary>
        /// <param name="rdModel">制单参数</param>
        /// <returns></returns> 
        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost]
        [MyAuthorizeAttribute] 
        public ApiReturnModel<object> AddVoucherLogin([FromBody] CoVoucherModel<DispatchListRd32AddModel> rdModel)
        {
            UserModel user = (UserModel)HttpContext.Current.Items["User"]; 
            ApiReturnModel<object> rj;
            try
            {
                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", rdModel.ToString(), "获取发货单列表完成").SQLLog(); //日志输出 

                IEnumerable<Rdrecord32Model> dmModel;
                using (IDbConnection conn = new SqlConnection(GetU8DbConnection))
                {
                    dmModel = conn.Query<Rdrecord32Model, Rdrecords32Model, Rdrecord32Model>($@"
select 
{DateTime.Now:yyyy-MM-dd} dDate
,'' cCode
,'制单人' cMaker
,'87' VT_ID 
,'0' bRdFlag
,'32' cVouchType
,'普通销售' cBusType
,'发货单' cSource
,dl.cDLCode cBusCode
,dls.cWhCode

,cRdCode
,dl.cDepCode
,dl.cPersonCode
,dl.cSTCode
,dl.cCusCode
,dl.cDLCode 

,dls.cInvCode 
,dls.iQuantity 
,'0' iFlag 
,iDLsID     --发货退货单子表标识
,dls.iSOsID  iorderdid  --订单子表id 
,so.csvouchtype iordertype --订单类型
,dls.cSoCode iordercode --订单号
,sode.iRowNo iorderseq  --销售订单行号 
,'1' isotype    --订单类型

,dls.cdemandid ipesodid   --pe需求id 
,dls.idemandtype ipesotype  --pe需求类型 
,dls.cdemandcode cpesocode  --pe件需求跟踪号 
,idemandseq ipesoseq   --pe需求行号
,'' irowno 
from DispatchList dl
join DispatchLists dls on dl.DLID=dls.DLID
left join SO_SODetails sode on sode.iSOsID=dls.iSOsID 
left join SO_SOMain so on so.ID=sode.ID
where dls.iDLsID=''", (course, location) =>
                        {
                            if (course.body == null)
                                course.body = new List<Rdrecords32Model>();
                            course.body.Add(location);
                            return course;
                        }, splitOn: "DLID");
                }

                Rdrecord32Model rdrecord32 = dmModel.GetEnumerator().Current;

                foreach (var rdrecords32Model in rdrecord32.body)
                {
                    foreach (var dispatchListRd32SAddModel in rdModel.VoucherData.BodyList)
                    {
                        if (rdrecords32Model.iDLsID == dispatchListRd32SAddModel.iDLsID)
                        {
                            rdrecords32Model.iQuantity = dispatchListRd32SAddModel.iQuantity;
                        }
                    } 
                }

                CoVoucherModel < Rdrecord32Model > rdVoucherModel = new CoVoucherModel<Rdrecord32Model>();
                rdVoucherModel.Login = rdModel.Login;
                rdVoucherModel.VoucherData = rdrecord32;
                rj = base.AddVoucherLogin(rdVoucherModel);

                // new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", dlModel.ToString(), "获取发货单列表完成").SQLLog(); //日志输出 
                 
                //if (rj.Flag == 1)
                //{
                //    Database U8db = HzyaDBFactory.DBFactory.CreateDatabase(DatabaseType.SQL, null, _STCO.U8LoginUfDataConnstringForNet);

                //    ExecuteResult<int> ex = U8db.ExecuteNonQuery($@" update h set h.cSaleOut='ST' from   dispatchlist h 
                //    inner join dispatchlists d on h.dlid = d.dlid
                //    inner join rdrecords32 rs on rs.idlsid = d.idlsid
                //    where isnull(h.cSaleOut,'')=''  and rs.ID = '{rj.Msg}'");
                //    if (ex.ExecuteResultType != ExecuteResultType.True) return new ApiReturnModel<object>(1, ex.ErrorMessage, "", $"{rj.Msg}");
                //}
            }
            catch (Exception e)
            {
               // Log4Helper.Error(this.GetType(), $"{DateTime.Now.ToString("O")}:{_TITLE} Exception:{e.ToString()},data:{rdModel.ToString()} ");
                new ApiLogModel(this.GetType(), ErrorType.警告, user.UserName, "", rdModel.ToString(), "出库单异常:"+ e.ToString()).SQLLog(); //日志输出 
                return new ApiReturnModel<object>(0, e.Message);
            }
            new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, rj.ToString(), rdModel.ToString(), "出库单完成:").SQLLog(); //日志输出 
             
            return rj;
        }

        /// <summary>
        /// 删除指定单据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost]
        [MyAuthorizeAttribute]
        public  ApiReturnModel<object> DeleteVoucher(JObject json)
        {
            return base.DeleteVoucherLogin(json);
        }
    }
}
