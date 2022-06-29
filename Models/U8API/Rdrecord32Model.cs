using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API.Models.U8API
{
    public class DispatchListRd32AddModel
    {
        /// <summary>
        /// 单据号
        /// </summary>
        public string cCode { get; set; }

        /// <summary>
        /// 发货单主表id
        /// </summary>
        public string DLID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 数据
        /// </summary>

        public List<DispatchListRd32sAddModel> BodyList { get; set; }

        public override string ToString()
        {
            string bodyString = "";
            foreach (var rdrecords32Model in BodyList)
            {
                bodyString = $"{rdrecords32Model.ToString()},";
            }

            return $"{nameof(cCode)}: {cCode}, {nameof(DLID)}: {DLID}, {nameof(BodyList)}: [{bodyString}]";
        }
    }
    public class DispatchListRd32sAddModel
    {  
        /// <summary>
        /// 子表标识
        /// </summary>
        public string iDLsID { get; set; }
        /// <summary>
        /// 数量(主计量)
        /// </summary>
        public decimal iQuantity { get; set; }
         
        public override string ToString()
        {
            return $"{nameof(iDLsID)}: {iDLsID??""??""}, {nameof(iQuantity)}: {iQuantity}";
        }
    }



    public class Rdrecord32Model
    {
        /// <summary>
        /// 必输 收发单据号
        /// </summary>
        public string cCode { get; set; }//(U8100)  收发单据号 nvarchar 30  False
        /// <summary>
        /// 必输 业务类型 默认
        /// </summary>
        public string cBusType { get; set; }//(U8100)  业务类型 nvarchar 12  True 

        /// <summary>
        /// 必输 单据来源
        /// </summary>
        public string cSource { get; set; }//(U8100)   nvarchar 50  False

        /// <summary>
        /// 必输 单据类型编码
        /// </summary>
        public string cVouchType { get; set; }//(U8100)  单据类型编码 nvarchar 2  False
        /// <summary>
        /// 收发类别编码
        /// </summary>
        public string cRdCode { get; set; }//(U8100)  收发类别编码 nvarchar 5  True
        /// <summary>
        /// 单据日期
        /// </summary>
        public string ddate { get; set; }//(U8100)  单据日期 datetime 8  False 
        /// <summary>
        /// 到货日期
        /// </summary>
        public string dARVDate { get; set; }//(U8100)  到货日期 datetime 8  True
        /// <summary>
        /// 审核日期
        /// </summary>
        public string dVeriDate { get; set; }//(U8100)  审核日期 datetime 8  True
        /// <summary>
        /// 必输  制单人  
        /// </summary>
        public string cMaker { get; set; }//(U8100)  制单人 nvarchar 20  True  
        /// <summary>
        /// 必输 收发表示
        /// </summary>
        public string bRdFlag { get; set; }//(U8100)  收发标志 tinyint 1  False 

        /// <summary>
        /// 备注
        /// </summary>
        public string cMemo { get; set; }//(U8100)  备注 nvarchar 255  True  
        /// <summary>
        /// 必输 单据模版号 默认值
        /// </summary>
        public string VT_ID { get; set; }//(U8100)  单据模版号 int 4  True
        /// <summary>
        /// 产量
        /// </summary>
        public string iMQuantity { get; set; }//(U8100)  产量 float 8  True
        /// <summary>
        /// 检验员
        /// </summary>
        public string cChkPerson { get; set; }//(U8100)  检验员 nvarchar 40  True   
        /// <summary>
        /// 必输 对应业务单号
        /// </summary>
        public string cBusCode { get; set; }//(U8100)  对应业务单号 nvarchar 30  True
        /// <summary>
        /// 必输 发货退货单主表标识
        /// </summary>
        public string cDLCode { get; set; }//(U8100)  发货退货单主表标识 bigint 8  True

        /// <summary>
        /// 必输 销售类型编码
        /// </summary>
        public string cSTCode { get; set; }//(U8100)  销售类型编码 nvarchar 2  True
        /// <summary>
        /// 必输 仓库编码
        /// </summary>
        public string cWhCode { get; set; }//(U8100)  仓库编码 nvarchar 10  False 


        /// <summary>
        ///  币种名称
        /// </summary>
        public string cExch_Name { get; set; }//(U8100)  币种名称 nvarchar 8  True 
        /// <summary>
        /// 自定义项目15
        /// </summary>
        public string cDefine15 { get; set; }//(U8100)  自定义项15 int 4  True 
        /// <summary>
        /// 自定义项目9
        /// </summary>
        public string cDefine9 { get; set; }//(U8100)  自定义项9 nvarchar 8  True
        /// <summary>
        /// 自定义项目8
        /// </summary>
        public string cDefine8 { get; set; }//(U8100)  自定义项8 nvarchar 4  True
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cVenCode { get; set; }//(U8100)  供应商编码 nvarchar 20  True 
        /// <summary>
        /// 自定义项目13
        /// </summary>
        public string cDefine13 { get; set; }//(U8100)  自定义项13 nvarchar 120  True
        /// <summary>
        /// 自定义项目1
        /// </summary>
        public string cDefine1 { get; set; }//(U8100)  自定义项1 nvarchar 20  True 
        /// <summary>
        /// 自定义项目5
        /// </summary>

        public string cDefine5 { get; set; }//(U8100)  自定义项5 int 4  True
        /// <summary>
        /// 发货地址
        /// </summary>
        public string cShipAddress { get; set; }//(U8100)  发货地址 nvarchar 200  True 

        /// <summary>
        /// 是否自检
        /// </summary>
        public string gspcheck { get; set; }//(U8100)  是否自检 nvarchar 10  True
        /// <summary>
        /// 自定义项目3
        /// </summary>
        public string cDefine3 { get; set; }//(U8100)  自定义项3 nvarchar 20  True

        /// <summary>
        /// 必输 客户编码
        /// </summary>
        public string cCusCode { get; set; }//(U8100)  客户编码 nvarchar 20  True
        /// <summary>
        /// 必输 开票单位
        /// </summary>
        public string cinvoicecompany { get; set; }//(U8111)  开票单位 nvarchar 20  True
        /// <summary>
        /// 快递单号
        /// </summary>
        public string cEBExpressCode { get; set; }//(U8111)  快递单号 nvarchar 50  True
        /// <summary>
        /// 生产批号
        /// </summary>
        public string cProBatch { get; set; }//(U8100)  生产批号 nvarchar 50  True  
        /// <summary>
        /// 自定义项目7
        /// </summary>
        public string cDefine7 { get; set; }//(U8100)  自定义项7 float 8  True 

        /// <summary>
        /// 部门编码
        /// </summary>
        public string cDepCode { get; set; }//(U8100)  部门编码 nvarchar 12  True
        /// <summary>
        /// 发票号
        /// </summary>
        public string isalebillid { get; set; }//(U8100)  发票号 nvarchar 30  True
        /// <summary>
        /// 检验单号
        /// </summary>
        public string cChkCode { get; set; }//(U8100)  检验单号 nvarchar 30  True
        /// <summary>
        /// 采购到货单号
        /// </summary>
        public string cARVCode { get; set; }//(U8100)  采购到货单号 nvarchar 30  True
        /// <summary>
        /// 当前审批人
        /// </summary>
        public string cCurrentAuditor { get; set; }//(U8110)  当前审批人 nvarchar 200  True
        /// <summary>
        /// 存货期初标志
        /// </summary>
        public string biafirst { get; set; }//(U8100)  存货期初标志 bit 1  True
        /// <summary>
        ///  采购类型编码
        /// </summary>
        public string cPTCode { get; set; }//(U8100)  采购类型编码 nvarchar 2  True 
        /// <summary>
        /// 自定义项目12
        /// </summary>
        public string cDefine12 { get; set; }//(U8100)  自定义项12 nvarchar 120  True
        /// <summary>
        /// 自定义项目2
        /// </summary>
        public string cDefine2 { get; set; }//(U8100)  自定义项2 nvarchar 20  True 
        /// <summary>
        /// 采购期初标志
        /// </summary>
        public string bpufirst { get; set; }//(U8100)  采购期初标志 bit 1  True
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string cOrderCode { get; set; }//(U8100)  采购订单号 nvarchar 30  True
        /// <summary>
        /// 自定义项4
        /// </summary>
        public string cDefine4 { get; set; }//(U8100)  自定义项4 datetime 8  True
        /// <summary>
        /// 汇率
        /// </summary>
        public string iExchRate { get; set; }//(U8100)  汇率 float 8  True
        /// <summary>
        /// 修改人
        /// </summary>
        public string cModifyPerson { get; set; }//(U8100)  修改人 nvarchar 50  True
        /// <summary>
        /// 零售来源类型
        /// </summary>
        public string cSourceLs { get; set; }//(U8100)  零售来源类型 nvarchar 4  True
        /// <summary>
        /// 自定义项6
        /// </summary>
        public string cDefine6 { get; set; }//(U8100)  自定义项6 datetime 8  True
        /// <summary>
        /// 扣税类别
        /// </summary>
        public string iDiscountTaxType { get; set; }//(U8100)  扣税类别 tinyint 1  True
        /// <summary>
        /// 自定义项10
        /// </summary>
        public string cDefine10 { get; set; }//(U8100)  自定义项10 nvarchar 60  True
        /// <summary>
        /// 检验日期
        /// </summary>
        public string dChkDate { get; set; }//(U8100)  检验日期 datetime 8  True 
        /// <summary>
        /// 自定义项16
        /// </summary>
        public string cDefine16 { get; set; }//(U8100)  自定义项16 float 8  True
        /// <summary>
        /// 收发记录主表标识
        /// </summary>
        public string ID { get; set; }//(U8100)  收发记录主表标识 bigint 8  False
        /// <summary>
        /// 必输 业务员编码
        /// </summary>
        public string cPersonCode { get; set; }//(U8100)  业务员编码 nvarchar 20  True
        /// <summary>
        /// 自定义项14
        /// </summary>
        public string cDefine14 { get; set; }//(U8100)  自定义项14 nvarchar 120  True
        /// <summary>
        /// 委外期初
        /// </summary>
        public string bOMFirst { get; set; }//(U8100)  委外期初 bit 1  True 
        /// <summary>
        /// 自定义项11
        /// </summary>
        public string cDefine11 { get; set; }//(U8100)  自定义项11 nvarchar 120  True 
        /// <summary>
        /// 收货地址编码收货地址编码收货地址编码
        /// </summary>
        public string caddcode { get; set; }//(U8100)  收货地址编码收货地址编码收货地址编码 nvarchar 60  True
        /// <summary>
        /// 记账人
        /// </summary>
        public string cAccounter { get; set; }//(U8100)  记账人

        public string DLID { get; set; }
         

        public override string ToString()
        {
            //string bodyString = "";
            //foreach (var rdrecords32Model in body)
            //{
            //    bodyString = $"{rdrecords32Model.ToString()},";
            //}

            return $"{nameof(cCode)}: {cCode??""}, {nameof(cBusType)}: {cBusType ?? ""}, {nameof(cSource)}: {cSource ??""}, {nameof(cVouchType)}: {cVouchType ?? ""??""}, {nameof(cRdCode)}: {cRdCode ?? ""}, {nameof(ddate)}: {ddate ?? ""}, {nameof(dARVDate)}: {dARVDate ?? ""}, {nameof(dVeriDate)}: {dVeriDate ?? ""}, {nameof(cMaker)}: {cMaker ??""}, {nameof(bRdFlag)}: {bRdFlag??""}, {nameof(cMemo)}: {cMemo??""}, {nameof(VT_ID)}: {VT_ID??""}, {nameof(iMQuantity)}: {iMQuantity??""}, {nameof(cChkPerson)}: {cChkPerson??""}, {nameof(cBusCode)}: {cBusCode??""}, {nameof(cDLCode)}: {cDLCode??""}, {nameof(cSTCode)}: {cSTCode??""}, {nameof(cWhCode)}: {cWhCode??""}, {nameof(cExch_Name)}: {cExch_Name??""}, {nameof(cDefine15)}: {cDefine15??""}, {nameof(cDefine9)}: {cDefine9??""}, {nameof(cDefine8)}: {cDefine8??""}, {nameof(cVenCode)}: {cVenCode??""}, {nameof(cDefine13)}: {cDefine13??""}, {nameof(cDefine1)}: {cDefine1??""}, {nameof(cDefine5)}: {cDefine5??""}, {nameof(cShipAddress)}: {cShipAddress??""}, {nameof(gspcheck)}: {gspcheck??""}, {nameof(cDefine3)}: {cDefine3??""}, {nameof(cCusCode)}: {cCusCode??""}, {nameof(cinvoicecompany)}: {cinvoicecompany??""}, {nameof(cEBExpressCode)}: {cEBExpressCode??""}, {nameof(cProBatch)}: {cProBatch??""}, {nameof(cDefine7)}: {cDefine7??""}, {nameof(cDepCode)}: {cDepCode??""}, {nameof(isalebillid)}: {isalebillid??""}, {nameof(cChkCode)}: {cChkCode??""}, {nameof(cARVCode)}: {cARVCode??""}, {nameof(cCurrentAuditor)}: {cCurrentAuditor??""}, {nameof(biafirst)}: {biafirst??""}, {nameof(cPTCode)}: {cPTCode??""}, {nameof(cDefine12)}: {cDefine12??""}, {nameof(cDefine2)}: {cDefine2??""}, {nameof(bpufirst)}: {bpufirst??""}, {nameof(cOrderCode)}: {cOrderCode??""}, {nameof(cDefine4)}: {cDefine4??""}, {nameof(iExchRate)}: {iExchRate??""}, {nameof(cModifyPerson)}: {cModifyPerson??""}, {nameof(cSourceLs)}: {cSourceLs??""}, {nameof(cDefine6)}: {cDefine6??""}, {nameof(iDiscountTaxType)}: {iDiscountTaxType??""}, {nameof(cDefine10)}: {cDefine10??""}, {nameof(dChkDate)}: {dChkDate??""}, {nameof(cDefine16)}: {cDefine16??""}, {nameof(ID)}: {ID??""}, {nameof(cPersonCode)}: {cPersonCode??""}, {nameof(cDefine14)}: {cDefine14??""}, {nameof(bOMFirst)}: {bOMFirst??""}, {nameof(cDefine11)}: {cDefine11??""}, {nameof(caddcode)}: {caddcode??""}, {nameof(cAccounter)}: {cAccounter??""}";
        }
    }
    public class Rdrecords32Model
    {
        public string DLID { get; set; }
        /// <summary>
        /// 重要 出库单类型
        /// </summary>
        public string coutvouchtype { get; set; }//(U8100)   nvarchar 2  True
        /// <summary>
        /// 重要 销售订单子表ID
        /// </summary>
        public string isodid { get; set; }//(U8100)   nvarchar 40  True
        /// <summary>
        /// 重要 销售订单行号
        /// </summary>
        public string isoseq { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 原始入库单类型
        /// </summary>
        public string coritracktype { get; set; }//(U8100)   nvarchar 2  True

        /// <summary>
        /// 重要 发货退货单子表标识
        /// </summary>
        public string iDLsID { get; set; }//(U8100)   bigint 8  True
        /// <summary>
        /// 必须 数量
        /// </summary>
        public decimal iQuantity { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 重要 单价
        /// </summary>
        public string iUnitCost { get; set; }//(U8100)   decimal 17  True 


        /// <summary>
        /// 必须 入库单单类型 默认
        /// </summary>
        public string cinvouchtype { get; set; }//(U8100)   nvarchar 2  True
        /// <summary>
        /// 必须 单据体行号
        /// </summary>
        public int irowno { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 必须  存货编码
        /// </summary>
        public string cInvCode { get; set; }//(U8100)   nvarchar 60  False
        /// <summary>
        /// 存货自由项1
        /// </summary>
        public string cFree1 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 重要 辅计量数量
        /// </summary>
        public string iNum { get; set; }//(U8100)    17  True

        /// <summary>
        /// 重要 金额
        /// </summary>

        public string iPrice { get; set; }//(U8100)   money 8  True

        /// <summary>
        /// 重要 换算率
        /// </summary>
        public string iinvexchrate { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 重要 批号
        /// </summary>
        public string cBatch { get; set; }//(U8100)  批号 nvarchar 60  True
        /// <summary>
        /// 重要 订单号
        /// </summary>
        public string iordercode { get; set; }//(U8100)  订单号 nvarchar 30  True
        /// <summary>
        /// 重要 销售订单号
        /// </summary>
        public string csocode { get; set; }//(U8100)   nvarchar 40  True
        /// <summary>
        /// 重要 项目编码
        /// </summary>
        public string cItemCode { get; set; }//(U8100)   nvarchar 60  True
        /// <summary>
        /// 重要 项目大类名称
        /// </summary>
        public string cItemCName { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 重要 项目大类编码
        /// </summary>
        public string cItem_class { get; set; }//(U8100)   nvarchar 10  True
        /// <summary>
        /// 项目名称
        /// </summary>
        public string cName { get; set; }//(U8100)   nvarchar 255  True   


        /// <summary>
        /// 存货自由项6
        /// </summary>
        public string cFree6 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 存货自由项4
        /// </summary>
        public string cFree4 { get; set; }//(U8100)  存货自由项4 nvarchar 20  True



        /// <summary>
        /// 实际辅计量数量
        /// </summary>
        public string iFNum { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 表体自定义项23
        /// </summary>
        public string cDefine23 { get; set; }//(U8100)   nvarchar 60  True
        /// <summary>
        /// 不良品处理单号
        /// </summary>
        public string cRejectCode { get; set; }//(U8100)   nvarchar 30  True

        public string bCosting { get; set; }//(U8100)  单据是否核算 bit 1  True
        public string dCheckDate { get; set; }//(U8100)  检验日期 datetime 8  True

        /// <summary>
        /// 销售订单行号
        /// </summary>
        public string iorderseq { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 收发记录子表标识
        /// </summary>
        public string AutoID { get; set; }//(U8100)   bigint 8  False
        /// <summary>
        /// 对应入库单子表标识
        /// </summary>
        public string cVouchCode { get; set; }//(U8100)   bigint 8  True
        /// <summary>
        /// 有效期推算方式
        /// </summary>
        public string iExpiratDateCalcu { get; set; }//(U8100)   smallint 2  True
        /// <summary>
        /// 表体自定义项25
        /// </summary>
        public string cDefine25 { get; set; }//(U8100)   nvarchar 60  True   
        public string cDefine35 { get; set; }//(U8100)  表体自定义项35 int 4  True  
        /// <summary>
        /// 订单类型
        /// </summary>
        public string iordertype { get; set; }//(U8100)   tinyint 1  True  
        /// <summary>
        /// pe件需求跟踪号
        /// </summary>
        public string cpesocode { get; set; }//(U8100)   nvarchar 40  True
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cBVencode { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 订单类型
        /// </summary>
        public string isotype { get; set; }//(U8100)   tinyint 1  True
        /// <summary>
        /// 检验单子表标识
        /// </summary>
        public string iCheckIds { get; set; }//(U8100)   bigint 8  True
        /// <summary>
        /// 设备字表id
        /// </summary>

        public string iEqDID { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// pe需求类型
        /// </summary>
        public string ipesotype { get; set; }//(U8100)   tinyint 1  True
        /// <summary>
        /// 库管员名称
        /// </summary>
        public string cwhpersonname { get; set; }//(U8100)   nvarchar 50  True
        /// <summary>
        /// 发货单号
        /// </summary>
        public string cbdlcode { get; set; }//(U8100)   nvarchar 30  True
        /// <summary>
        /// 生产日期
        /// </summary>
        public string dMadeDate { get; set; }//(U8100)   datetime 8  True 
        /// <summary>
        /// 订单子表id
        /// </summary>
        public string iorderdid { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 存货自由项9
        /// </summary>
        public string cFree9 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 存货自由项5
        /// </summary>
        public string cFree5 { get; set; }//(U8100)   nvarchar 20  True    
        /// <summary>
        /// 是否指定货位
        /// </summary>
        public string iposflag { get; set; }//   smallint 2  True 
        public string cDefine28 { get; set; }//(U8100)  表体自定义项28 nvarchar 120  True
        /// <summary>
        /// 不良品处理单ID
        /// </summary>
        public string iRejectIds { get; set; }//(U8100)   bigint 8  True 
        /// <summary>
        /// pe需求id
        /// </summary>
        public string ipesodid { get; set; }//(U8100)   nvarchar 40  True
        /// <summary>
        /// 库户存货编码
        /// </summary>
        public string ccusinvcode { get; set; }//(U8100)   nvarchar 40  True
        /// <summary>
        /// 已开票退货数量
        /// </summary>
        public string fretqtyykp { get; set; }//(U8110)   decimal 13  True
        /// <summary>
        /// 表体自定义项34
        /// </summary>

        public string cDefine34 { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 表体记账日期
        /// </summary>
        public string dbKeepDate { get; set; }//(U8100)   datetime 8  True
        /// <summary>
        /// 代管消耗标识
        /// </summary>

        public string bVMIUsed { get; set; }//(U8100)   bit 1  True
        /// <summary>
        /// 表体自定义项26
        /// </summary>
        public string cDefine26 { get; set; }//(U8100)   float 8  True 
        /// <summary>
        /// 保质期单位
        /// </summary>
        public string cMassUnit { get; set; }//(U8100)   smallint 2  True
        /// <summary>
        /// 实际数量
        /// </summary>
        public string iFQuantity { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 委托代销发货单子表标识
        /// </summary>
        public string iEnsID { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 未开票退货数量
        /// </summary>
        public string fretqtywkp { get; set; }//(U8110)   decimal 13  True
        /// <summary>
        /// 应收应发数量
        /// </summary>
        public string iNQuantity { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 是否销售出库开票
        /// </summary>
        public string bsaleoutcreatebill { get; set; }//(U8111)   bit 1  True
        /// <summary>
        /// 应收应发辅计量数量
        /// </summary>
        public string iNNum { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 有效期至
        /// </summary>
        public string cExpirationdate { get; set; }//(U8100)   varchar 10  True
        /// <summary>
        /// 存货自由项7
        /// </summary>
        public string cFree7 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 存货自由项2
        /// </summary>
        public string cFree2 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 是否需要开票
        /// </summary>
        public string bneedbill { get; set; }//(U8111)   bit 1  True
        /// <summary>
        /// 有效期计算项
        /// </summary>
        public string dExpirationdate { get; set; }//(U8100)   datetime 8  True
        /// <summary>
        /// 本次发货数量
        /// </summary>
        public string iSendQuantity { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 表体自定义项29
        /// </summary>
        public string cDefine29 { get; set; }//(U8100)   nvarchar 120  True 
        /// <summary>
        /// 累计退货数量
        /// </summary>
        public string iSRedOutQuantity { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 存货自由项3
        /// </summary>
        public string cFree3 { get; set; }//(U8100)   nvarchar 20  True

        /// <summary>
        /// 累计出库数量
        /// </summary>
        public string iSOutQuantity { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 检验员
        /// </summary>
        public string cCheckPersonCode { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 检验单子表ID
        /// </summary>
        public string iCheckIdBaks { get; set; }//(U8100)   bigint 8  True

        /// <summary>
        /// 计划价或售价
        /// </summary>
        public string iPUnitCost { get; set; }//(U8100)   decimal 17  True

        /// <summary>
        /// 表体自定义项24
        /// </summary>
        public string cDefine24 { get; set; }//(U8100)   nvarchar 60  True

        /// <summary>
        /// 发票子表标识
        /// </summary>
        public string iSBsID { get; set; }//(U8100)   bigint 8  True
        /// <summary>
        /// 合同标标识
        /// </summary>
        public string strContractId { get; set; }//(U8100)   nvarchar 64  True
        /// <summary>
        /// 存货自由项10
        /// </summary>
        public string cFree10 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// pe需求行号
        /// </summary>
        public string ipesoseq { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 是否存货核算开票
        /// </summary>
        public string bIAcreatebill { get; set; }//(U8111)   bit 1  True
        /// <summary>
        /// 序列号数量
        /// </summary>
        public string iInvSNCount { get; set; }//(U8100)   int 4  True
        /// <summary>
        /// 是否质检
        /// </summary>
        public string bGsp { get; set; }//(U8100)   bit 1  True
        /// <summary>
        /// 表体自定义项32
        /// </summary>
        public string cDefine32 { get; set; }//(U8100)   nvarchar 120  True 
        /// <summary>
        /// 对应入库单号
        /// </summary>
        public string cInVouchCode { get; set; }//(U8100)   nvarchar 30  True
        /// <summary>
        /// 销售出库单是否报检
        /// </summary>
        public string bChecked { get; set; }//(U8100)   bit 1  True
        /// <summary>
        /// 行时间戳
        /// </summary>
        public string rowufts { get; set; }//(U8100)   timestamp 8  True
        /// <summary>
        /// 表体自定义项31
        /// </summary>
        public string cDefine31 { get; set; }//(U8100)   nvarchar 120  True
        /// <summary>
        /// 服务单id
        /// </summary>
        public string cbserviceoid { get; set; }//(U8100)   varchar 38  True
        /// <summary>
        /// 表体自定义项37
        /// </summary>
        public string cDefine37 { get; set; }//(U8100)   datetime 8  True
        /// <summary>
        /// 合同号
        /// </summary>
        public string strCode { get; set; }//(U8100)   nvarchar 64  True
        /// <summary>
        /// 出库单id
        /// </summary>
        public string coutvouchid { get; set; }//(U8100)   bigint 8  True
        /// <summary>
        /// 累计出库辅计量数量
        /// </summary>
        public string iSOutNum { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 暂估金额
        /// </summary>
        public string iAPrice { get; set; }//(U8100)   money 8  True
        /// <summary>
        /// 单据行条码
        /// </summary>
        public string cbsysbarcode { get; set; }//(U8110)   nvarchar 80  True
        /// <summary>
        /// 检验单号
        /// </summary>
        public string cCheckCode { get; set; }//(U8100)   nvarchar 30  True
        /// <summary>
        /// 辅计量单位编码
        /// </summary>
        public string cAssUnit { get; set; }//(U8100)   nvarchar 35  True 

        /// <summary>
        /// 表体自定义项22
        /// </summary>
        public string cDefine22 { get; set; }//(U8100)   nvarchar 60  True
        /// <summary>
        /// 借出还回单子表id
        /// </summary>
        public string iDebitIDs { get; set; }//(U8101)   int 4  True
        /// <summary>
        /// 代管结算数量
        /// </summary>
        public string iVMISettleQuantity { get; set; }//(U8100)   decimal 17  True


        /// <summary>
        /// 库管员编码
        /// </summary>
        public string cwhpersoncode { get; set; }//(U8100)   nvarchar 20  True

        /// <summary>
        /// 累计退货件数
        /// </summary>
        public string iSRedOutNum { get; set; }//(U8100)   decimal 17  True 
        /// <summary>
        /// 表体自定义项36
        /// </summary>
        public string cDefine36 { get; set; }//(U8100)   datetime 8  True
        /// <summary>
        /// 客户存货名称
        /// </summary>
        public string ccusinvname { get; set; }//(U8100)   nvarchar 100  True
        /// <summary>
        /// 表体自定义项33
        /// </summary>
        public string cDefine33 { get; set; }//(U8100)   nvarchar 120  True
        /// <summary>
        /// 存货自由项8
        /// </summary>
        public string cFree8 { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 货位编码
        /// </summary>
        public string cPosition { get; set; }//(U8100)   nvarchar 20  True
        /// <summary>
        /// 对应条形码编码
        /// </summary>
        public string cBarCode { get; set; }//(U8100)   nvarchar 200  True
        /// <summary>
        /// 表体备注
        /// </summary>
        public string cbMemo { get; set; }//(U8100)   nvarchar 255  True
        /// <summary>
        /// 标志
        /// </summary>
        public string iFlag { get; set; }//(U8100)   tinyint 1  False
        /// <summary>
        /// 表体自定义项30
        /// </summary>
        public string cDefine30 { get; set; }//(U8100)   nvarchar 120  True
        /// <summary>
        /// 卸载前消抵数量
        /// </summary>
        public string ipreuseqty { get; set; }//(U8100)   decimal 17  True
        /// <summary>
        /// 表体自定义项27
        /// </summary>
        public string cDefine27 { get; set; }//(U8100)    8  True
        /// <summary>
        /// 已开票数量
        /// </summary>
        public string fsettleqty { get; set; }//(U8110)    13  True   
        /// <summary>
        /// 计划金额或售价金额
        /// </summary>
        public string iPPrice { get; set; }//(U8100)   money 8  True
        /// <summary>
        /// 代管商编码
        /// </summary>
        public string cvmivencode { get; set; }//(U8100)   nvarchar 20  True

        public override string ToString()
        {
            return $"{nameof(coutvouchtype)}: {coutvouchtype??""}, {nameof(isodid)}: {isodid??""}, {nameof(isoseq)}: {isoseq??""}, {nameof(coritracktype)}: {coritracktype??""}, {nameof(iDLsID)}: {iDLsID??""}, {nameof(iQuantity)}: {iQuantity}, {nameof(iUnitCost)}: {iUnitCost??""}, {nameof(cinvouchtype)}: {cinvouchtype??""}, {nameof(irowno)}: {irowno}, {nameof(cInvCode)}: {cInvCode??""}, {nameof(cFree1)}: {cFree1??""}, {nameof(iNum)}: {iNum??""}, {nameof(iPrice)}: {iPrice??""}, {nameof(iinvexchrate)}: {iinvexchrate??""}, {nameof(cBatch)}: {cBatch??""}, {nameof(iordercode)}: {iordercode??""}, {nameof(csocode)}: {csocode??""}, {nameof(cItemCode)}: {cItemCode??""}, {nameof(cItemCName)}: {cItemCName??""}, {nameof(cItem_class)}: {cItem_class??""}, {nameof(cName)}: {cName??""}, {nameof(cFree6)}: {cFree6??""}, {nameof(cFree4)}: {cFree4??""}, {nameof(iFNum)}: {iFNum??""}, {nameof(cDefine23)}: {cDefine23??""}, {nameof(cRejectCode)}: {cRejectCode??""}, {nameof(bCosting)}: {bCosting??""}, {nameof(dCheckDate)}: {dCheckDate??""}, {nameof(iorderseq)}: {iorderseq??""}, {nameof(AutoID)}: {AutoID??""}, {nameof(cVouchCode)}: {cVouchCode??""}, {nameof(iExpiratDateCalcu)}: {iExpiratDateCalcu??""}, {nameof(cDefine25)}: {cDefine25??""}, {nameof(cDefine35)}: {cDefine35??""}, {nameof(iordertype)}: {iordertype??""}, {nameof(cpesocode)}: {cpesocode??""}, {nameof(cBVencode)}: {cBVencode??""}, {nameof(isotype)}: {isotype??""}, {nameof(iCheckIds)}: {iCheckIds??""}, {nameof(iEqDID)}: {iEqDID??""}, {nameof(ipesotype)}: {ipesotype??""}, {nameof(cwhpersonname)}: {cwhpersonname??""}, {nameof(cbdlcode)}: {cbdlcode??""}, {nameof(dMadeDate)}: {dMadeDate??""}, {nameof(iorderdid)}: {iorderdid??""}, {nameof(cFree9)}: {cFree9??""}, {nameof(cFree5)}: {cFree5??""}, {nameof(iposflag)}: {iposflag??""}, {nameof(cDefine28)}: {cDefine28??""}, {nameof(iRejectIds)}: {iRejectIds??""}, {nameof(ipesodid)}: {ipesodid??""}, {nameof(ccusinvcode)}: {ccusinvcode??""}, {nameof(fretqtyykp)}: {fretqtyykp??""}, {nameof(cDefine34)}: {cDefine34??""}, {nameof(dbKeepDate)}: {dbKeepDate??""}, {nameof(bVMIUsed)}: {bVMIUsed??""}, {nameof(cDefine26)}: {cDefine26??""}, {nameof(cMassUnit)}: {cMassUnit??""}, {nameof(iFQuantity)}: {iFQuantity??""}, {nameof(iEnsID)}: {iEnsID??""}, {nameof(fretqtywkp)}: {fretqtywkp??""}, {nameof(iNQuantity)}: {iNQuantity??""}, {nameof(bsaleoutcreatebill)}: {bsaleoutcreatebill??""}, {nameof(iNNum)}: {iNNum??""}, {nameof(cExpirationdate)}: {cExpirationdate??""}, {nameof(cFree7)}: {cFree7??""}, {nameof(cFree2)}: {cFree2??""}, {nameof(bneedbill)}: {bneedbill??""}, {nameof(dExpirationdate)}: {dExpirationdate??""}, {nameof(iSendQuantity)}: {iSendQuantity??""}, {nameof(cDefine29)}: {cDefine29??""}, {nameof(iSRedOutQuantity)}: {iSRedOutQuantity??""}, {nameof(cFree3)}: {cFree3??""}, {nameof(iSOutQuantity)}: {iSOutQuantity??""}, {nameof(cCheckPersonCode)}: {cCheckPersonCode??""}, {nameof(iCheckIdBaks)}: {iCheckIdBaks??""}, {nameof(iPUnitCost)}: {iPUnitCost??""}, {nameof(cDefine24)}: {cDefine24??""}, {nameof(iSBsID)}: {iSBsID??""}, {nameof(strContractId)}: {strContractId??""}, {nameof(cFree10)}: {cFree10??""}, {nameof(ipesoseq)}: {ipesoseq??""}, {nameof(bIAcreatebill)}: {bIAcreatebill??""}, {nameof(iInvSNCount)}: {iInvSNCount??""}, {nameof(bGsp)}: {bGsp??""}, {nameof(cDefine32)}: {cDefine32??""}, {nameof(cInVouchCode)}: {cInVouchCode??""}, {nameof(bChecked)}: {bChecked??""}, {nameof(rowufts)}: {rowufts??""}, {nameof(cDefine31)}: {cDefine31??""}, {nameof(cbserviceoid)}: {cbserviceoid??""}, {nameof(cDefine37)}: {cDefine37??""}, {nameof(strCode)}: {strCode??""}, {nameof(coutvouchid)}: {coutvouchid??""}, {nameof(iSOutNum)}: {iSOutNum??""}, {nameof(iAPrice)}: {iAPrice??""}, {nameof(cbsysbarcode)}: {cbsysbarcode??""}, {nameof(cCheckCode)}: {cCheckCode??""}, {nameof(cAssUnit)}: {cAssUnit??""}, {nameof(cDefine22)}: {cDefine22??""}, {nameof(iDebitIDs)}: {iDebitIDs??""}, {nameof(iVMISettleQuantity)}: {iVMISettleQuantity??""}, {nameof(cwhpersoncode)}: {cwhpersoncode??""}, {nameof(iSRedOutNum)}: {iSRedOutNum??""}, {nameof(cDefine36)}: {cDefine36??""}, {nameof(ccusinvname)}: {ccusinvname??""}, {nameof(cDefine33)}: {cDefine33??""}, {nameof(cFree8)}: {cFree8??""}, {nameof(cPosition)}: {cPosition??""}, {nameof(cBarCode)}: {cBarCode??""}, {nameof(cbMemo)}: {cbMemo??""}, {nameof(iFlag)}: {iFlag??""}, {nameof(cDefine30)}: {cDefine30??""}, {nameof(ipreuseqty)}: {ipreuseqty??""}, {nameof(cDefine27)}: {cDefine27??""}, {nameof(fsettleqty)}: {fsettleqty??""}, {nameof(iPPrice)}: {iPPrice??""}, {nameof(cvmivencode)}: {cvmivencode}";
        }
    }

    /// <summary>
    /// 删除用实体
    /// </summary>
    public class DeleteRd32
    {
         
        /// <summary>
        ///  
        /// </summary>
        public string rd32ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rd10ID { get; set; }
         
    }

}