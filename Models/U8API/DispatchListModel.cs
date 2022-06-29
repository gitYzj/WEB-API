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
        /// 编码 int
        /// </summary>
       public string DLID { get; set; }
        /// <summary>
        /// 单据号 模糊 like
        /// </summary>
        public string cDLCode { get; set; }
        /// <summary>
        /// 单据日期 yyyy-MM-dd
        /// </summary>
        public string dDateStart { get; set; }
        /// <summary>
        /// 单据日期 yyyy-MM-dd
        /// </summary>
        public string dDateEnd { get; set; }

        /// <summary>
        /// 审核时间 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string dverifysystimeStart { get; set; }
        /// <summary>
        /// 审核时间 yyyy-MM-dd hh:mm:ss
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


        /// <summary>
        /// 客户名称 模糊 like
        /// </summary>
        public string cCusName { get; set; }

        /// <summary>
        /// 存货名称 模糊 like
        /// </summary>
        public string cInvName { get; set; }
 


        public override string ToString()
        { 
            return $"{nameof(DLID)}: {DLID}, {nameof(cDLCode)}: {cDLCode}, {nameof(dDateStart)}: {dDateStart}, {nameof(dDateEnd)}: {dDateEnd}, {nameof(dverifysystimeStart)}: {dverifysystimeStart}, {nameof(dverifysystimeEnd)}: {dverifysystimeEnd}, {nameof(bReturnFlag)}: {bReturnFlag}, {nameof(iCloser)}: {iCloser}";
        }
    }

    /// <summary>
    /// 发货单
    /// </summary>
    public class DispatchListModel
    {
        /// <summary>
        /// 发货退货单主表标识
        /// </summary>
        public int DLID { get; set; }
        /// <summary>
        /// 发货退货单号
        /// </summary>
        public string cDLCode { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>

        public DateTime dDate { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        /// <returns></returns>
        public string cDepCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        public string cDepName { get; set; }
        /// <summary>
        /// 业务员编码
        /// </summary>
        public string cPersonCode { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string cPsn_Name { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string cexch_name { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>

        public decimal iExchRate { get; set; }
        /// <业务类型>
        /// cbustype
        /// </summary>
        public string cbustype { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime dcreatesystime { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime dverifysystime { get; set; }
        /// <summary>
        /// 是否退货
        /// </summary>
        public int bReturnFlag { get; set; }
        /// <summary>
        /// VouchState
        /// </summary>
        public string VouchState { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string cCusCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string cCusName { get; set; }
        /// <summary>
        /// 收发类别
        /// </summary>
        public string cRdCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string cMemo { get; set; }
         
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
        public int DLID { get; set; }
        /// <summary>
        /// 发货单子表标识
        /// </summary>
        public string iDLsID { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string cWhName { get; set; }
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
        /// 主计量单位
        /// </summary>
        public string  cComunitName { get; set; }
        /// <summary>
        /// 主计量
        /// </summary>
        public decimal iQuantity { get; set; }
        /// <summary>
        /// 辅助计量单位
        /// </summary>
        public string cAccComunitName { get; set; }
        /// <summary>
        /// 辅助计量
        /// </summary>
        public decimal iNum { get; set; }
        /// <summary>
        /// 项目号
        /// </summary>
        public string cItemCode { get; set; }
        /// <summary>
        /// 项目大类
        /// </summary>
        public string cItem_class { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string cSoCode { get; set; }
        
        /// <summary>
        /// 批号
        /// </summary>
        public string cBatch { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string cMemo { get; set; }
        

    }
}