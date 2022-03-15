using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace WEB_API.Models.U8API
{
    /// <summary>
    /// 发货单查询条件
    /// </summary>
    public class DispatchListGetModel
    {
        /// <summary>
        /// 编码
        /// </summary>
       public string DLID { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public string cDLCode { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string dDateStart { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string dDateEnd { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string dverifysystimeStart { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string dverifysystimeEnd { get; set; }
        /// <summary>
        /// 是否退货
        /// </summary>
        public string bReturnFlag { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public string iCloser { get; set; }
    }

    /// <summary>
    /// 发货单
    /// </summary>
    public class DispatchListModel
    {
        /// <summary>
        /// 发货退货单主表标识
        /// </summary> 
        public string DLID { get; set; }

        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public string AutoID { get; set; }
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public string iDLsID { get; set; }

        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode { get; set; }
         
        /// <summary>
        /// 发货退货单号
        /// </summary>
        public string cDLCode  { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string dDate { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cDepCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string cDepName { get; set; }
        /// <summary>
        /// 业务员编码
        /// </summary>
        public string cPersonCode { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string cexch_name { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public string iExchRate { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string cbustype { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        public string dcreatesystime { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public string dverifysystime { get; set; }
        /// <summary>
        /// 是否退货
        /// </summary>
        public string bReturnFlag { get; set; }
        public string cDefine4 { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string VouchState { get; set; }

        /// <summary>
        /// 收发类别
        /// </summary>
        public string cRdCode { get; set; }
        /// <summary>
        /// 销售类型编码
        /// </summary>
        public string cSTCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string cMemo { get; set; }
        /// <summary>
        /// 
        /// </summary>
       public string cinvoicecompany { get; set; }
        /// <summary>
        /// 
        /// </summary>
       public string cInvoicecusname { get; set; }
     
        /// <summary>
        /// 表体
        /// </summary>
        public List<DispatchListsModel> body { get; set; }=new List<DispatchListsModel>();
    }
    /// <summary>
    /// 发货单表体
    /// </summary>
    public class DispatchListsModel
    {
        /// <summary>
        /// 发货退货单主表标识
        /// </summary>
        public string DLID { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string cmodifier { get; set; }
        /// <summary>
        /// 销售订单子表标识
        /// </summary>
        public string iSOsID { get; set; }
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string cSoCode { get; set; }
        /// <summary>
        /// 需求跟踪方式（1-销售订单行号；4-需求分类号；5-销售订单号）
        /// </summary>
        public string idemandtype { get; set; }//需求跟踪方式（1-销售订单行号；4-需求分类号；5-销售订单号）
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public string iDLsID { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode { get; set; }
        /// <summary>
        /// 存货编码 
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 存货名称 
        /// </summary>
        public string cInvName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string cInvStd { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string iQuantity { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public string iNum { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string cItemCode { get; set; }
        /// <summary>
        /// 项目大类编码
        /// </summary>
        public string cItem_class { get; set; }
        /// <summary>
        /// 原币无税金额
        /// </summary>
        public string iMoney { get; set; }
        /// <summary>
        /// 原币税额
        /// </summary>
        public string iTax { get; set; }
        /// <summary>
        /// 原币价税合计
        /// </summary>
        public string iSum { get; set; }
        /// <summary>
        /// 原币折扣额 
        /// </summary>
        public string iDisCount { get; set; }
        /// <summary>
        /// 本币无税单价
        /// </summary>
        public string iNatUnitPrice { get; set; }
        /// <summary>
        /// 本币无税金额 
        /// </summary>
        public string iNatMoney { get; set; }
        /// <summary>
        /// 本币税额
        /// </summary>
        public string iNatTax { get; set; }
        /// <summary>
        /// 本币价税合计 
        /// </summary>
        public string iNatSum { get; set; }
        /// <summary>
        /// 本币折扣额 
        /// </summary>
        public string iNatDisCount { get; set; }
        /// <summary>
        /// 订单行号 
        /// </summary>
        public string iorderrowno { get; set; }//||订单行号 
        /// <summary>
        /// 批号
        /// </summary>
        public string cBatch { get; set; }//批号
        /// <summary>
        /// 备注
        /// </summary>
        public string cMemo { get; set; }//备注

    }
}