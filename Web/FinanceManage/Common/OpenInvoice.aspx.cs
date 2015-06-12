using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.TourStructure;

namespace Web.FinanceManage.Common
{
    /// <summary>
    /// 销售-财务
    /// 开票
    /// 公共页面
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-3-19
    /// 参数说明:
    /// * OrderId = 订单号
    /// * TourId = 团队编号
    /// 订单号,团队编号 选一个
    /// 父级页面类型
    /// ParentType = 1 财务
    /// ParentType = 2 销售
    public partial class OpenInvoice : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetFormValue("doType").Length > 0)
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void DataInit()
        {
            string orderId = Utils.GetQueryStringValue("OrderId") == "" ? Utils.GetQueryStringValue("Id") : Utils.GetQueryStringValue("OrderId");
            if (orderId != "")
            {
                IList<MBill> sl = new BFinance().GetBillLst(orderId);
                if (sl != null && sl.Count > 0)
                {
                    rpt_list.DataSource = sl;
                    rpt_list.DataBind();
                }
                var info = new BTourOrder().GetOrderMoney(orderId);
                if (info != null)
                {
                    lbl_listTitle.Text = "应收金额：" + UtilsCommons.GetMoneyString(info.ConfirmMoney, ProviderToMoney)
                        + "  已审开票金额：" + UtilsCommons.GetMoneyString(info.BillAmount, ProviderToMoney);
                }
            }
            else
            {
                Utils.TopRedirect();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {

            string msg = string.Empty;
            BFinance bll = new BFinance();
            switch (Utils.GetFormValue("doType"))
            {
                case "ExamineV":
                    bool retBool = bll.SetApproveBill(CurrentUserCompanyID,Utils.GetInt(Utils.GetFormValue("Id")),true,SiteUserInfo.UserId,SiteUserInfo.Name,string.Empty,DateTime.Now,Utils.GetFormValue("BillNo"));
                    if (retBool)
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "审核失败!"));
                    }

                    break;
                case "Delete":
                    if (bll.DelBill(CurrentUserCompanyID,Utils.GetInt(Utils.GetFormValue("Id"))))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "删除失败!"));
                    }

                    break;
                default:/*添加修改*/
                    msg = UpdateModel();
                    if (msg != "")
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1", "开票成功!"));
                    }
                    break;
            }
            bll = null;
        }
        /// <summary>
        /// 获取参数并验证
        /// </summary>
        /// <param name="model">赋值实体</param>
        /// <param name="msg">验证提示语</param>
        /// <returns></returns>
        private string UpdateModel()
        {
            BFinance bll = new BFinance();
            MBill model = new MBill();
            string msg = string.Empty;

            #region 表单验证
            if (Utils.GetFormValue("doType") == "Update" && Utils.GetFormValue("Id").Trim() == "")
            {
                msg = "无法修改该数据!<br/>";
            }

            if (Utils.GetDateTimeNullable(Utils.GetFormValue("openInvoiceDate")) == null)
            {
                msg += "请输入开票时间!<br/>";
            }

            if (Utils.GetDecimal(Utils.GetFormValue("openInvoiceMoney")) == 0)
            {
                msg += "请输入开票金额!";
            }

            if (msg != "")
            {
                return msg;
            }
            #endregion

            #region 实体赋值
            model.Id = Utils.GetInt(Utils.GetFormValue("Id"), 0);
            model.TourId = Utils.GetFormValue("TourId");
            model.TourCode = Utils.GetFormValue("TourCode");
            if (model.TourCode.Length <= 0)
            {
                MTourBaseInfo tourModel = new BTour().GetTourInfo(model.TourId);
                if (tourModel != null)
                {
                    model.TourCode = tourModel.TourCode;
                }
            }
            //订单编号
            model.OrderId = Utils.GetFormValue("OrderId");
            //开票单位Id
            model.CustomerId = Utils.GetFormValue("CustomerId");
            //开票单位
            model.Customer = Utils.GetFormValue("Customer");
            //销售员
            model.SellerName = Server.UrlDecode(Utils.GetFormValue("SellerName"));
            model.SellerId = Utils.GetFormValue("SellerId");
            //开票人
            model.DealerId = SiteUserInfo.UserId;
            model.Dealer = SiteUserInfo.Name;
            //操作员名称
            model.Operator = SiteUserInfo.Name;
            model.OperatorId = SiteUserInfo.UserId;
            //客户单位联系电话
            model.ContactName = Server.UrlDecode(Utils.GetFormValue("Contact"));
            model.ContactPhone = Utils.GetFormValue("Phone");
            //操作时间
            model.IssueTime = DateTime.Now;
            //操作员部门编号
            model.DeptId = SiteUserInfo.DeptId;
            //系统公司
            model.CompanyId = CurrentUserCompanyID;
            //开票时间
            model.BillTime = Utils.GetDateTime(Utils.GetFormValue("openInvoiceDate"));
            //开票金额
            model.BillAmount = Utils.GetDecimal(Utils.GetFormValue("openInvoiceMoney"));
            model.Remark = Utils.GetFormValue("openInvoiceRemar");
            #endregion

            if (!bll.AddOrUpdBill(model))
            {
                msg = "开票失败!";
            }
            return msg;
        }

        /// <summary>
        /// 栏目权限控制
        /// </summary>
        protected void PowerControl()
        {
            if (Utils.GetQueryStringValue("sl") == "")
            {
                Utils.TopRedirect("/default.aspx");
            }

            EyouSoft.Model.EnumType.PrivsStructure.Menu2 m = (EyouSoft.Model.EnumType.PrivsStructure.Menu2)Utils.GetInt(Utils.GetQueryStringValue("sl"));

            if (m == EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_应收管理)
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_栏目))
                {
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_开票登记))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_开票登记, false);
                    }
                }
                else
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_应收管理_栏目, false);
                }
            }
            else if (m == EyouSoft.Model.EnumType.PrivsStructure.Menu2.财务管理_杂费收入)
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_杂费收入_栏目))
                {
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_杂费收入_开票登记))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_杂费收入_开票登记, false);
                    }
                }
                else
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_杂费收入_栏目, false);
                }
            }
            else if (m == EyouSoft.Model.EnumType.PrivsStructure.Menu2.销售中心_销售收款)
            {
                if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目))
                {
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_开票登记))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_开票登记, false);
                    }
                }
                else
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售收款_栏目, false);
                }
            }
            else
            {
                Utils.TopRedirect("/default.aspx");
            }
        }
    }
}
