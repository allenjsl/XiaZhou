using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyousoft.Common.Page;
using EyouSoft.Common;

namespace Web.FinanceManage.OneTeamAdjustAccounts
{
    /// <summary>
    /// 利润分配-添加-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    public partial class AddDistributeProfit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //根据ID初始化页面
                PageInit(id);
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {

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
            //分配类型
            string distributeType = Utils.InputText(txt_distributeType.Text);
            //金额
            string money = Utils.InputText(txt_Money.Text);
            //部门Id
            string sectionId = Utils.InputText(SelectSection1.SectionID);
            //销售员Id
            string sellsId = Utils.InputText(SellsSelect1.SellsID);
            bool test = distributeType.Length > 0 && money.Length > 0 && sectionId.Length > 0;
        }
    }
}
