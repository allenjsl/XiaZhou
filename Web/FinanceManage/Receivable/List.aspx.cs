using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.EnumType.FinStructure;
using EyouSoft.Model.EnumType.PrivsStructure;


namespace Web.FinanceManage.Receivable
{
    /// <summary>
    /// 应收账款
    /// 应收已结清
    /// 公用
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    public partial class List : BackPage
    {
        #region attributes
        /// <summary>
        /// 单项业务游客确认单路径
        /// </summary>
        protected string PrintPage_DanXiangYeWuYouKeQueRenDan = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            PrintPage_DanXiangYeWuYouKeQueRenDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务游客确认单);

            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            //初始化
            DataInit();

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            #region 查询实体
            MReceivableBase queryModel = GetChaXunInfo();
            #endregion
            //金额汇总信息
            MReceivableSum sum = new MReceivableSum();
            IList<MReceivableInfo> ls = new BFinance().GetReceivableInfoLst(
                pageSize,
                pageIndex,
                ref  recordCount,
                ref sum,
                queryModel);

            lbl_totalBill.Text = UtilsCommons.GetMoneyString(sum.TotalBill, ProviderToMoney);
            lbl_totalReceived.Text = UtilsCommons.GetMoneyString(sum.TotalReceived, ProviderToMoney);
            lbl_totalReturned.Text = UtilsCommons.GetMoneyString(sum.TotalReturned, ProviderToMoney);
            lbl_totalSumPrice.Text = UtilsCommons.GetMoneyString(sum.TotalSumPrice, ProviderToMoney);
            lbl_totalUnchecked.Text = UtilsCommons.GetMoneyString(sum.TotalUnchecked, ProviderToMoney);
            lbl_totalUnChkReturn.Text = UtilsCommons.GetMoneyString(sum.TotalUnChkReturn, ProviderToMoney);
            lbl_totalUnReceived.Text = UtilsCommons.GetMoneyString(sum.TotalUnReceived, ProviderToMoney);

            if (ls != null && ls.Count > 0)
            {
                pan_sum.Visible = true;
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_应收管理_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_杂费收入_栏目, true);
                return;
            }
            else
            {
                pan_plshoukuan.Visible = CheckGrant(Privs.财务管理_应收管理_收款登记);

                pan_plshenhe.Visible = CheckGrant(Privs.财务管理_应收管理_收款审核);

                pan_plkaipiao.Visible = CheckGrant(Privs.财务管理_应收管理_开票登记);

                pan_DRSK.Visible = CheckGrant(Privs.财务管理_应收管理_查看当日收款);
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;
            //金额汇总信息
            MReceivableSum sum = new MReceivableSum();
            MReceivableBase queryModel  = GetChaXunInfo();
            IList<MReceivableInfo> ls = new BFinance().GetReceivableInfoLst(
                UtilsCommons.GetToXlsRecordCount(),
                1,
                ref  recordCount,
                ref sum,
                queryModel);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("订单号\t线路名称\t客源单位\t客源单位部门\t客源单位联系人\t");
                sb.Append("销售员\t下单人\t金额\t状态\t已收金额\t");
                sb.Append("已收待审\t欠款金额\t已退金额\t已退待审金额\t开票金额\n");

                foreach (MReceivableInfo item in ls)
                {
                    sb.Append(item.OrderCode + "\t");
                    sb.Append(item.RouteName + "\t");
                    sb.Append(item.Customer + "\t");
                    sb.Append(item.KeHuDeptName + "\t");
                    sb.Append(item.Contact + "\t");
                    sb.Append(item.Salesman + "\t");
                    sb.Append(item.OperatorName + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Receivable, ProviderToMoney) + "\t");
                    sb.Append((item.IsConfirmed ? "已确认" : "未确认") + item.TourType + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Received, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.UnChecked, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.UnReceived, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Returned, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.UnChkRtn, ProviderToMoney) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.Bill, ProviderToMoney) + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }

        /// <summary>
        /// 获取查询信息
        /// </summary>
        /// <returns></returns>
        MReceivableBase GetChaXunInfo()
        {
            MReceivableBase info = new MReceivableBase();
            info.CompanyId = CurrentUserCompanyID;
            info.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("SDate"));
            info.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EDate"));
            info.OrderCode = Utils.GetQueryStringValue("orderId");//订单号
            info.Customer = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHMC);//客户单位
            info.CustomerId = Utils.GetQueryStringValue(CustomerUnitSelect1.ClientNameKHBH);
            info.IsClean = Utils.GetQueryStringValue("isReceived") == "1";//是否已结清
            info.Salesman = txt_Seller.SellsName = Utils.GetQueryStringValue(txt_Seller.SellsNameClient);//销售员
            info.SalesmanId = txt_Seller.SellsID = Utils.GetQueryStringValue(txt_Seller.SellsIDClient);//销售员
            //已收待审金额
            info.SignUnChecked = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator));
            info.UnChecked = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber));
            //未收金额
            info.SignUnReceived = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            info.UnReceived = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));

            info.OperatorId = txtXiaDanRen.SellsID = Utils.GetQueryStringValue(txtXiaDanRen.SellsIDClient);
            info.OperatorName = txtXiaDanRen.SellsName = Utils.GetQueryStringValue(txtXiaDanRen.SellsNameClient);

            if (Utils.GetQueryStringValue("txtHeTongJinEQueRenStatus") == "1") info.HeTongJinEQueRenStatus = true;
            if (Utils.GetQueryStringValue("txtHeTongJinEQueRenStatus") == "0") info.HeTongJinEQueRenStatus = false;
            info.ShouKuanRenId = Utils.GetQueryStringValue(txtShouKuanRen.SellsIDClient);
            info.ShluKuanRenName = Utils.GetQueryStringValue(txtShouKuanRen.SellsNameClient);

            info.ShouKuanSTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtShouKuanSTime"));
            info.ShouKuanETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtShouKuanETime"));

            info.RouteName = Utils.GetQueryStringValue("txtRouteName");
            info.JiDiaoYuanId = txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsIDClient);
            info.JiDiaoYuanName = txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            return info;
        }
        #endregion
    }
}
