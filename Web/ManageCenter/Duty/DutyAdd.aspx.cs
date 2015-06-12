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

namespace Web.ManageCenter.Duty
{
    /// <summary>
    /// 行政中心-职务管理-添加
    /// </summary>
    /// 修改人：方琪
    /// 创建时间：2012-03-19
    public partial class DutyAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            if (doType == "checkDutyName")
            {
                CheckDutyName();
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }
        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            BPosition BLL = new BPosition();
            MGovPosition Model = BLL.GetGovPositionModel(Utils.GetInt(id), this.SiteUserInfo.CompanyId);
            if (null != Model)
            {
                this.txtDutyName.Text = Model.Title;//职位名
                this.txtContent.Text = Model.Description;//职位说明     
                this.hidDutyId.Value = Model.PositionId.ToString();
                if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                {
                    this.ph_Save.Visible = false;
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string msg = "";
            bool result = false;
            Response.Clear();
            //职位名称
            string dutyName = Utils.GetFormValue(txtDutyName.UniqueID);
            //职位说明书
            string content = Utils.GetFormValue(txtContent.UniqueID);
            //职位编号
            string dutyId = Utils.GetFormValue(hidDutyId.UniqueID);
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(dutyName))
            {
                msg += "-请输入职务名称！";
            }
            if (string.IsNullOrEmpty(content))
            {
                msg += "-请输入职务描述！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                result = false;
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
                return;
            }
            #endregion

            #region 提交回应
            BPosition BLL = new BPosition();
            MGovPosition Model;
            if (doType == "add")
            {
                Model = new MGovPosition();
                this.commonModel(Model, dutyName, content);
                result = BLL.AddGovPosition(Model);
                msg = result ? "添加成功！" : "添加失败！";
            }
            if (doType == "update")
            {
                Model = BLL.GetGovPositionModel(Utils.GetInt(dutyId), this.SiteUserInfo.CompanyId);
                if (null != Model)
                {
                    this.commonModel(Model, dutyName, content);
                    Model.PositionId = Int32.Parse(dutyId);
                    result = BLL.UpdateGovPosition(Model);
                    msg = result ? "修改成功！" : "修改失败！";
                }
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion

        }
        #endregion

        #region 共用Model部分
        /// <summary>
        /// 新增与编辑共用Model部分
        /// </summary>
        protected void commonModel(MGovPosition Model, string dutyName, string content)
        {
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.Description = content;
            Model.IssueTime = DateTime.Now;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.Title = dutyName;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_职务管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_职务管理_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "update")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_职务管理_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_职务管理_修改, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_职务管理_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_职务管理_新增, false);
                    }
                }
            }



        }
        #endregion

        #region 检查职位名是否存在
        /// <summary>
        /// 检查职位名是否存在
        /// </summary>
        protected void CheckDutyName()
        {
            string str = String.Empty;
            string id = Utils.GetQueryStringValue("id");
            string name = Utils.GetQueryStringValue("name");
            if (!string.IsNullOrEmpty(name))
            {
                BPosition BLL = new BPosition();
                if (String.IsNullOrEmpty(id))
                {//新增
                    if (BLL.ExistsNum(name, 0, this.SiteUserInfo.CompanyId))
                    {
                        str = "1";
                    }
                }
                else
                { //更新
                    MGovPosition Model = BLL.GetGovPositionModel(Utils.GetInt(id), this.SiteUserInfo.CompanyId);
                    if (null != Model && !string.Equals(name, Model.Title))
                    {
                        if (BLL.ExistsNum(name, 0, this.SiteUserInfo.CompanyId))
                        {
                            str = "1";
                        }
                    }
                }
            }
            Response.Clear();
            Response.Write(str);
            Response.End();
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