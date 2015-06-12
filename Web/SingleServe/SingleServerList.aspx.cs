using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;

namespace Web.SingleServe
{
    /// <summary>
    /// 单项业务
    /// 创建人：刘飞
    /// 时间：2011-10-21
    /// </summary>
    public partial class SingleServerList : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        protected string NodataMsg = string.Empty;
        protected string StatusList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "delete": Delete(); break;
                case "quxiao": QuXiao(); break;
                default: break;
            }

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //订单编号
                string OrderId = Utils.GetQueryStringValue("txtOrderId");
                //单位名称
                string UnitName = Utils.GetQueryStringValue("txtUnitName");
                //下单开始时间
                string OrderSTime = Utils.GetQueryStringValue("txtOrderSTime");
                //下单结束时间
                string OrderETime = Utils.GetQueryStringValue("txtOrderETime");
                //客户单位
                string comId = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHBH);
                string comName = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);
                int status = Utils.GetInt(Utils.GetQueryStringValue("status"), -1);
                this.CustomerUnitSelect1.CustomerUnitId = comId;
                this.CustomerUnitSelect1.CustomerUnitName = comName;
                //销售员
                string opeaterId = Utils.GetQueryStringValue(this.SellsSelect1.SellsIDClient);
                string opeaterName = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
                this.SellsSelect1.SellsID = opeaterId;
                this.SellsSelect1.SellsName = opeaterName;
                //初始化
                DataInit(OrderId, UnitName, OrderSTime, OrderETime, opeaterName, comName, status);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string orderid, string unitname, string orderstime, string orderetime, string operater, string customunit, int status)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.TourStructure.BSingleService bll = new EyouSoft.BLL.TourStructure.BSingleService();
            EyouSoft.Model.TourStructure.MSeachSingleService model = new EyouSoft.Model.TourStructure.MSeachSingleService();
            model.BeginLDate = Utils.GetDateTimeNullable(orderstime);
            model.TourStatus = status == -1 ? null : (EyouSoft.Model.EnumType.TourStructure.TourStatus?)status;
            model.EndLDate = Utils.GetDateTimeNullable(orderetime);
            model.BuyCompanyName = customunit;
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.Operator = operater;
            model.OrderCode = orderid;
            model.SWeiTuoRiQi = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSWeiTuoRiQi"));
            model.EWeiTuoRiQi = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEWeiTuoRiQi"));

            model.XiaoShouYuanId =this.txtXiaoShouYuan.SellsID = Utils.GetQueryStringValue(this.txtXiaoShouYuan.SellsIDClient);
            model.JiDiaoYuanId = this.txtJiDiaoYuan.SellsID = Utils.GetQueryStringValue(this.txtJiDiaoYuan.SellsIDClient);

            model.XiaoShouYuanName = this.txtXiaoShouYuan.SellsName = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            model.JiDiaoYunaName = this.txtJiDiaoYuan.SellsName = Utils.GetQueryStringValue(txtJiDiaoYuan.SellsNameClient);

            IList<MSingleService> list = bll.GetSingleServiceList(model, pageSize, pageIndex, ref recordCount);
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                this.litMsg.Text = "<tr class='old'><td colspan='12' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
            //绑定分页
            BindPage();
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_栏目, false);
                return;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        void Delete()
        {
            string txtTourId = Utils.GetFormValue("txtTourId");
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_删除)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：没有操作权限。"));

            string txtQuXiaoId = Utils.GetFormValue("txtQuXiaoId");

            int bllRetCode = new EyouSoft.BLL.TourStructure.BSingleService().Delete(CurrentUserCompanyID, txtTourId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：当前状态不允许删除。"));
            else if (bllRetCode == -97) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：已经存在收款，不允许删除。"));
            else if (bllRetCode == -96) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：已经存在付款，不允许删除。"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：异常代码[" + bllRetCode + "]"));
        }

        /// <summary>
        /// 取消
        /// </summary>
        void QuXiao()
        {
            if(!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.单项业务_单项业务_取消)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：没有操作权限。"));
            
            string txtQuXiaoId = Utils.GetFormValue("txtQuXiaoId");
            string txtYuanYin = Utils.GetFormValue("txtYuanYin");

            int bllRetCode = new EyouSoft.BLL.TourStructure.BSingleService().QuXiao(CurrentUserCompanyID, txtQuXiaoId, txtYuanYin, SiteUserInfo.UserId);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：当前状态不允许取消。"));
            else if (bllRetCode == -97) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：已经存在收款，不允许取消。"));
            else if (bllRetCode == -96) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：已经存在付款，不允许取消。"));
            else if (bllRetCode == -95) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：不是该团销售员或计调员，不允许取消。"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：异常代码[" + bllRetCode + "]"));
        }
        #endregion

        #region 前台调用方法
        protected string GetTourStatus(object obj,object yuanYin)
        {
            EyouSoft.Model.EnumType.TourStructure.TourStatus status = (EyouSoft.Model.EnumType.TourStructure.TourStatus)obj;
            string s = string.Empty;
            switch (status)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕: s = "已落实"; break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划: s = "操作中"; break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团: s = "核算结束"; break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消:
                    s = "<a data-class='QuXiaoYuanYin'><span class='fontgray' data-class='QuXiaoYuanYin' data-state='0'>已取消</span></a><div style='display: none'><b>取消原因</b>:" + yuanYin.ToString();
                    break;
                default: s = status.ToString(); break;
            }

            return s;
        }

        /// <summary>
        /// get hetong code
        /// </summary>
        /// <param name="heTongCode"></param>
        /// <returns></returns>
        protected string GetHeTongCode(object heTongCode)
        {
            string _heTongCode = heTongCode.ToString();

            if (string.IsNullOrEmpty(_heTongCode)) return "关联合同";
            return _heTongCode;
        }
        #endregion
    }
}
