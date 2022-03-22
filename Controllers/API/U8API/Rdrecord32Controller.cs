using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
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
        public ApiReturnModel<object> AddVoucherLogin([FromBody] CoVoucherModel<Rdrecord32Model> rdModel)
        {
            UserModel user = (UserModel)HttpContext.Current.Items["User"];
            //Log4Helper.Info(this.GetType(), $"{DateTime.Now.ToString("O")}:{_TITLE} Rdrecord32"); 
            ApiReturnModel<object> rj;
            try
            {
                new ApiLogModel(this.GetType(), ErrorType.日志, user.UserName, "", rdModel.ToString(), "获取发货单列表完成").SQLLog(); //日志输出 
                rj = base.AddVoucherLogin(rdModel);
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
         
    }
}
