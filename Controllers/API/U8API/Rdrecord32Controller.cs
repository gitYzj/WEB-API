using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HzyaMVCWebApiService.Controllers.ApiCO.Interface;
using HzyaU8COInterface.CO;
using Newtonsoft.Json.Linq;
using WEB_API.Models;
using WEB_API.Models.U8API;

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
        public ApiReturnModel<object> AddVoucherLogin([FromBody] CoVoucherModel<Rdrecord32Model> rdModel)
        {
            //C_Common.WriteLog("操作日志", $"{DateTime.Now.ToString("O")}:{_TITLE} AddVoucher");
            ApiReturnModel<object> rj;
            try
            {
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
                //C_Common.WriteLog("系统异常日志", $"{DateTime.Now.ToString("O")} GetVoucher:{_TITLE} Exception:{e.ToString()},data:{json} ");
                return new ApiReturnModel<object>(0, e.ToString());
            }
            //if (rj.Flag == 1)
            //C_Common.WriteLog(_TITLE, $"{DateTime.Now.ToString("O")}:{_TITLE} , return {rj.ToString()}");
            //else
            //C_Common.WriteLog(_TITLE, $"{DateTime.Now.ToString("O")}:{_TITLE} data:{json}, return {rj.ToString()}");
            return rj;
        }
         
    }
}
