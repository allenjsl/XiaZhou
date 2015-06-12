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

namespace Web.FinanceManage.Common
{
    /// <summary>
    /// 批量开票
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-3-19
    /// 参数说明:
    /// * OrderId = xxxxx,xxxxx 订单号集合
    /// * TourId = 团队编号
    /// 订单号,团队编号 选一个
    /// ParentPage = -1,//表示销售
    /// ParentPage = 1,//表示财务
    public partial class QuantityOpenInvoice : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            if (Utils.GetFormValue("doType") == "Save")
            {
                Save();
            }

            IList<MBill> ls = new BFinance().GetBillLst(Utils.GetQueryStringValue("OrderId").Split(',').ToList());
            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
            else
            {
                pan_sum.Visible = false;
                phdShow.Visible = false;
            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string[] strList = Utils.GetFormValue("list").Split(',');
            if (strList != null && strList.Count() > 0)
            {
                List<MBill> ls = new List<MBill>();
                foreach (string item in strList)
                {
                    string[] subData = item.Split('|');
                    if (subData.Length == 11)
                    {
                        ls.Add(new MBill
                        {
                            //系统公司编号
                            CompanyId = CurrentUserCompanyID,
                            BillTime = DateTime.Now,
                            //操作时间
                            IssueTime = DateTime.Now,
                            Dealer = SiteUserInfo.Name,
                            DealerId = SiteUserInfo.UserId,
                            Operator = SiteUserInfo.Name,
                            OperatorId = SiteUserInfo.UserId,
                            //订单编号
                            OrderId = subData[0],
                            //开票单位编号
                            CustomerId = subData[1],
                            //开票单位编号
                            Customer = subData[2],
                            //销售员编号
                            SellerId = subData[3],
                            //备注
                            Remark = subData[4],
                            //开票金额
                            BillAmount = Utils.GetDecimal(subData[5]),
                            SellerName = subData[6],
                            TourCode = subData[7],
                            TourId = subData[8],
                            ContactName = subData[9],
                            ContactPhone = subData[10]
                        });
                    }
                }
                IList<MBill> retls = new BFinance().AddOrUpdBill(ls);
                if (retls == null || retls.Count <= 0)
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                }
                else
                {
                    string strMsg = string.Empty;
                    foreach (var item in retls)
                    {
                        strMsg += item.OrderCode + "<br/>";
                    }
                    strMsg += "开票失败!";
                    AjaxResponse(UtilsCommons.AjaxReturnJson("-1", strMsg));
                }
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "无开票信息!"));
            }

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
