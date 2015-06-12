using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.MarketCenter
{
    /// <summary>
    /// 销售中心-销售收款列表页
    /// </summary>
    /// 汪奇志 2012-03-28
    public partial class XiaoShouShouKuan : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 页记录数
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        int recordCount = 0;
        /// <summary>
        /// 打印单链接
        /// </summary>
        protected string PrintPages = string.Empty;
        /// <summary>
        /// 单项业务游客确认单路径
        /// </summary>
        protected string PrintPage_DanXiangYeWuYouKeQueRenDan = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PrintPages = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单);
            PrintPage_DanXiangYeWuYouKeQueRenDan = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.单项业务游客确认单);
            
            if (!IsPostBack)
            {
                PowerControl();
                DataInit();
            }

            if (UtilsCommons.IsToXls()) ToXls();//导出
        }

        #region private members
        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();  

            var searchInfo = GetSearchInfo();
            var items = new EyouSoft.BLL.TourStructure.BTourOrder().GetXiaoShouShouKuanList(pageSize, pageIndex, ref recordCount, searchInfo);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }

            BindPage();
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            paging.Visible = paging.intRecordCount > paging.intPageSize;
            phEmpty.Visible = paging.intRecordCount == 0;
        }

        /// <summary>
        /// 导出
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            StringBuilder s = new StringBuilder();

            var searchInfo = GetSearchInfo();
            var items = new EyouSoft.BLL.TourStructure.BTourOrder().GetXiaoShouShouKuanList(toXlsRecordCount, 1, ref _recordCount, searchInfo);

            if (items != null && items.Count > 0)
            {
                s.Append("<table border=1>");
                s.Append("<tr><th align='center' rowspan='2'>团号-订单号</th>");
                s.Append("<th align='center' rowspan='2'>线路名称</th>");
                s.Append("<th align='center' rowspan='2'>人数</th>");
                s.Append("<th align='center' rowspan='2'>客源单位</th>");
                s.Append("<th align='center' rowspan='2'>客源单位部门</th>");
                s.Append("<th align='center' rowspan='2'>客源单位联系人</th>");
                s.Append("<th align='center' rowspan='2'>销售员</th>");
                s.Append("<th align='center' rowspan='2'>下单人</th>");
                s.Append("<th align='center' colspan='2'>合同金额</th>");
                s.Append("<th align='center' rowspan='2'>已收</th>");
                s.Append("<th align='center' rowspan='2'>已收待审</th>");
                s.Append("<th align='center' rowspan='2'>欠款</th>");
                s.Append("<th align='center' rowspan='2'>已退</th>");
                s.Append("<th align='center' rowspan='2'>已退待审</th>");
                s.Append("<th align='center' rowspan='2'>开票金额</th></tr>");
                s.Append("<tr><th align='center'>金额</th>");
                s.Append("<th align='center'>状态</th></tr>");
                foreach (var item in items)
                {
                    s.AppendFormat("<tr><td align='center'>{0}</td>", item.OrderCode);
                    s.AppendFormat("<td align='left'>{0}</td>", item.RouteName);
                    s.AppendFormat("<td align='center'><b>{0}</b><sup>+{1}</sup></td>", item.Adults, item.Childs);
                    s.AppendFormat("<td align='left'>{0}</td>", item.Customer);
                    s.AppendFormat("<td align='center'>{0}</td>", item.KeHuDeptName);
                    s.AppendFormat("<td align='center'>{0}</td>", item.Contact);
                    s.AppendFormat("<td align='center'>{0}</td>", item.Salesman);
                    s.AppendFormat("<td align='center'>{0}</td>", item.OperatorName);
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.Receivable, ProviderToMoney));
                    s.AppendFormat("<td align='center'>{0}</td>", (item.IsConfirmed ? "已确认" : "未确认") + item.TourType);
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.Received, ProviderToMoney));
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.UnChecked, ProviderToMoney));//
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.UnReceived, ProviderToMoney));
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.Returned, ProviderToMoney));
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.UnChkRtn, ProviderToMoney));
                    s.AppendFormat("<td align='right'>{0}</td>", UtilsCommons.GetMoneyString(item.Bill, ProviderToMoney));
                    s.Append("</tr>");
                }
            }
            ResponseToXls(s.ToString(), System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 获取查询参数
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.FinStructure.MReceivableBase GetSearchInfo()
        {
            var shouKuanStatus = Utils.GetQueryStringValue("txtShouKuanStatus");

            var info = new EyouSoft.Model.FinStructure.MReceivableBase();

            info.CompanyId = CurrentUserCompanyID;
            info.Customer=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHMC);
            info.CustomerId=Utils.GetQueryStringValue(txtKeHuDanWei.ClientNameKHBH);
            info.OrderCode = Utils.GetQueryStringValue("txtOrderCode");
            info.Salesman = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            info.SalesmanId = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient);
            info.OperatorId = Utils.GetQueryStringValue(txtXiaDanRen.SellsIDClient);
            info.OperatorName = Utils.GetQueryStringValue(txtXiaDanRen.SellsNameClient);

            if (shouKuanStatus == "1") info.IsClean = true;
            else if (shouKuanStatus == "0") info.IsClean = false;

            info.UnReceived = Utils.GetDecimalNull(Utils.GetQueryStringValue(txtQianKuan.ClientUniqueIDOperatorNumber));
            info.SignUnReceived = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(txtQianKuan.ClientUniqueIDOperator));

            info.UnChecked = Utils.GetDecimalNull(Utils.GetQueryStringValue(txtYiShouDaiShen.ClientUniqueIDOperatorNumber));
            info.SignUnChecked = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(txtYiShouDaiShen.ClientUniqueIDOperator));

            info.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLSDate"));
            info.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtLEDate"));

            if (Utils.GetQueryStringValue("txtHeTongJinEQueRenStatus") == "1") info.HeTongJinEQueRenStatus = true;
            if (Utils.GetQueryStringValue("txtHeTongJinEQueRenStatus") == "0") info.HeTongJinEQueRenStatus = false;
            info.ShouKuanRenId= Utils.GetQueryStringValue(txtShouKuanRen.SellsIDClient);
            info.ShluKuanRenName = Utils.GetQueryStringValue(txtShouKuanRen.SellsNameClient);

            info.ShouKuanSTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtShouKuanSTime"));
            info.ShouKuanETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtShouKuanETime"));

            info.RouteName = Utils.GetQueryStringValue("txtRouteName");
            info.JiDiaoYuanId = txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsIDClient);
            info.JiDiaoYuanName = txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            return info;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目, true);
                return;
            }
            this.phdPLKP.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_开票登记);
            this.phdPLSK.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_收款登记);
            this.phdDRDZD.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_查看当日收款);
        }
        #endregion
    }
}
