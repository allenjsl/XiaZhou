using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using Eyousoft.Common.Page;

namespace Web.FinanceManage.OneTeamAdjustAccounts
{
    /// <summary>
    /// 其他收入or其他支出-添加-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    public partial class AddDetail : BackPage
    {
        /// <summary>
        /// 收入支出类型
        /// </summary>
        protected string typeTitle = string.Empty;
        /// <summary>
        /// 收款付款方式
        /// </summary>
        protected string unitTitle = string.Empty;
        #region 私有常量定义收入支出
        /// <summary>
        /// 私有常量--收入
        /// </summary>
        private const string _typeincome = "收入";
        /// <summary>
        /// 私有常量--支出
        /// </summary>
        private const string _typepay = "支出";
        /// <summary>
        /// 私有常量--付款
        /// </summary>
        private const string _unitincome = "收款";
        /// <summary>
        /// 私有常量--收款
        /// </summary>
        private const string _unitpay = "付款";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();

            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            SetTitle();
        }
        /// <summary>
        /// 设置各种title
        /// </summary>
        private void SetTitle()
        {
            string toType = Utils.GetQueryStringValue("toType");
            //判断收入or支出
            if (toType == "income")
            {
                this.Title += _typeincome;
                typeTitle = _typeincome;
                unitTitle = _unitincome;
            }
            else
            {
                this.Title += _typepay;
                typeTitle = _typepay;
                unitTitle = _unitpay;
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {

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

        protected void lbtn_Save_Click(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            GetPageVal();
            bool result = true;


            if (result)
            {
                Utils.ShowMsgAndCloseBoxy("修改成功！", Utils.GetQueryStringValue("IframeId"), true);
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('修改失败!');");
            }
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        private void GetPageVal()
        {
            //收支类型
            string getsetType = Utils.InputText(txt_getsetType.Text);
            //收支单位
            string getsetUnit = Utils.InputText(txt_getsetUnit.Text);
            //金额
            string money = Utils.InputText(txt_Money.Text);
            bool test = getsetType.Length > 0 && getsetUnit.Length > 0 && money.Length > 0;
        }
    }
}
