using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.EnumType.GovStructure;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.Leave
{
    /// <summary>
    /// 行政中心-员工离职-审批
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-3-16
    public partial class Approve : BackPage
    {
        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            //存在ajax请求
            if (save == "approve")
            {
                PageApprove();
            }
            if (save == "save")
            {
                PageSave();
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="id"></param>
        protected void PageInit(string id)
        {
            BGovFilePersonnel BLL = new BGovFilePersonnel();
            //根据员工编号获取员工离职信息实体
            MGovFilePersonnel model = BLL.GetGovFilePersonnelModelByFileId(id);
            if (model != null)
            {
                //表单赋值、列表绑定
                this.lbFileName.Text = model.FileName;
                this.lbApproveCause.Text = model.Reason;
                this.lbApplicationTime.Text = string.Format("{0:yyyy-MM-dd}", model.ApplicationTime);
                if (model.GovPersonnelApproveList != null && model.GovPersonnelApproveList.Count > 0)
                {
                    this.rptapprove.DataSource = model.GovPersonnelApproveList;
                    this.rptapprove.DataBind();
                }
                if (model.ApproveState != ApprovalStatus.审核通过)
                {
                    ph_Save.Visible = false;
                }
                this.txtDepartureTime.Text = string.Format("{0:yyyy-MM-dd}", model.DepartureTime);
                if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != model.OperatorID)
                {
                    this.ph_Save.Visible = false;
                }
            }
        }
        #endregion

        #region 审批
        /// <summary>
        /// 审批
        /// </summary>
        protected void PageApprove()
        {
            string msg = "";
            bool result = false;
            //表单取值
            string id = Utils.GetQueryStringValue("id");
            string appid = Utils.GetQueryStringValue("appid");
            string appName = Utils.GetQueryStringValue("appname");
            string appTime = Utils.GetQueryStringValue("apptime");
            string appView = Utils.GetQueryStringValue("appview");
            //验证
            if (string.IsNullOrEmpty(appTime))
            {
                msg += "-请输入审批时间！<br/>";
            }
            if (string.IsNullOrEmpty(appView))
            {
                msg += "-请输入审批意见！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
            //实体赋值
            BGovFilePersonnel BLL = new BGovFilePersonnel();
            MGovPersonnelApprove model = new MGovPersonnelApprove();
            model.Id = id;
            model.ApproveID = appid;
            model.ApproveState = ApprovalStatus.审核通过;
            model.ApproveTime = Utils.GetDateTimeNullable(appTime);
            model.ApprovalViews = appView;
            model.ApproveName = appName;
            //提交保存
            result = BLL.UpdateGovFilePersonnel(model);
            msg = result ? "审批通过!" : "审批失败!";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }
        #endregion

        #region 离职保存
        /// <summary>
        /// 离职保存
        /// </summary>
        protected void PageSave()
        {
            string msg = "";
            bool result = false;
            string id = Utils.GetQueryStringValue("id");
            string departuretime = Utils.GetQueryStringValue("time");
            if (string.IsNullOrEmpty(departuretime))
            {
                msg += "-请输入员工离职时间！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
            BGovFilePersonnel BLL = new BGovFilePersonnel();

            result = BLL.UpdateIsLeft(Utils.GetDateTime(departuretime), id);
            msg = result ? "保存成功!" : "保存失败!";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_完成离职操作))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_完成离职操作, false);
            }

        }
        #endregion

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion


    }
}
