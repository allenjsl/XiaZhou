using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using System.Text;

/// <summary>
/// 行政中心-物品管理-物品归还[暂时不用此页面]
/// </summary>
/// 创建人：徐从栎
/// 创建时间：2011-10-21
namespace Web.ManageCenter.ObjectManage
{
    public partial class ObjLendBack : BackPage
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
            this.txtInTime.Value = string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now);
            this.lbHandler.Text = this.SiteUserInfo.Name;
            //编辑初始化
            if (String.Equals(Request.QueryString["dotype"], "update", StringComparison.InvariantCultureIgnoreCase))
            {
                BGood BLL = new BGood();
                //MGovGoodBorrow Model = BLL.GetGovGoodBorrowModel(id);
                //if (null != Model)
                //{
                //    this.lbName.Text = Model.Name;
                //    this.lbOutTime.Text = String.Format("{0:yyyy-MM-dd HH:mm}", Model.Time);
                //    this.lbPart.Text = Model.Dept;
                //    this.lbGetor.Text = Model.User;
                //    this.lbCount.Text = Convert.ToString(Model.Number);
                //    this.lbUse.Text = Model.Use;
                //    this.lbHandler.Text = Model.Operator;
                //    this.lbState.Text =Convert.ToString(EyouSoft.Model.EnumType.GovStructure.BorrowStatus.借阅中);
                //}
            }
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(String doType, String id)
        {
            //数据验证开始
            if (String.IsNullOrEmpty(this.txtInTime.Value))
            {
                MessageBox.ResponseScript(this, "alert('归还时间不能为空！');");
                return;
            }
            //数据验证结束
            BGood BLL = new BGood();
            //MGovGoodBorrow Model;
            int result = 0;
            //Model = BLL.GetGovGoodBorrowModel(id);
            //if (null != Model)
            //{
            //    Model.IssueTime = DateTime.Now;
            //    Model.Operator = this.SiteUserInfo.Name;
            //    Model.OperatorId = this.SiteUserInfo.UserId;
            //    Model.ReturnTime = Utils.GetDateTimeNullable(this.txtInTime.Value);
            //    result = BLL.UpdateGovGoodBorrow(Model);
            //}
            string m = "归还";
            switch (result)
            {
                case 1:
                    Utils.ShowMsgAndCloseBoxy(m + "成功！", Utils.GetQueryStringValue("IframeId"), true);
                    break;
                case 2:
                    MessageBox.ResponseScript(this, string.Format("alert('{0}失败,借阅数量超过库存!');", m));
                    break;
                default:
                    MessageBox.ResponseScript(this, string.Format("alert('{0}失败!');", m));
                    break;
            }
        }
        protected void btnClick(object sender, EventArgs e)
        {
            this.PageSave(Utils.GetQueryStringValue("doType"), Utils.GetQueryStringValue("id"));
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.行政中心_物品管理_借阅操作))
            {
                //Utils.ResponseNoPermit(Common.Enum.TravelPermission.行政中心_物品管理_借阅操作, false);
                return;
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
    }
}