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
using EyouSoft.Model.EnumType.GovStructure;

namespace Web.ManageCenter.Leave
{
    /// <summary>
    /// 行政中心-员工离职-添加申请
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-01
    public partial class LeaveAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion

            if (!IsPostBack)
            {
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
            BGovFilePersonnel BLL = new BGovFilePersonnel();
            MGovFilePersonnel model = BLL.GetGovFilePersonnelModel(id);
            if (model != null)
            {
                this.hidKeyId.Value = model.Id;
                this.HrSelect1.HrSelectName = model.FileName;
                this.HrSelect1.HrSelectID = model.FileId;
                this.txtApplyCause.Text = model.Reason;
                this.SellsSelect1.SellsID = GetApproveList(model.GovPersonnelApproveList, 2);
                this.SellsSelect1.SellsName = GetApproveList(model.GovPersonnelApproveList, 1);
                this.txtWantTime.Text = string.Format("{0:yyyy-MM-dd}", model.ApplicationTime);
                if (this.SiteUserInfo.UserId != model.OperatorID && !this.SiteUserInfo.IsHandleElse)
                {
                    this.ph_Save.Visible = false;
                }
            }
        }


        /// <summary>
        /// 获取审批人
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        protected string GetApproveList(IList<MGovPersonnelApprove> lst, int i)
        {
            string str = "";
            if (lst != null && lst.Count > 0)
            {
                switch (i)
                {
                    case 1:
                        foreach (var item in lst)
                        {
                            str += item.ApproveName + ",";
                        }
                        break;
                    case 2:
                        foreach (var item in lst)
                        {
                            str += item.ApproveID + ",";
                        }
                        break;
                    default:
                        break;
                }
                return str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string KeyId = Utils.GetFormValue(this.hidKeyId.UniqueID);
            string filename = Utils.GetFormValue(this.HrSelect1.HrSelectNameClient);
            string fileid = Utils.GetFormValue(this.HrSelect1.HrSelectIDClient);
            string reason = Utils.GetFormValue(this.txtApplyCause.UniqueID);
            string applicationtime = Utils.GetFormValue(this.txtWantTime.UniqueID);
            string personnelapprovename = Utils.GetFormValue(this.SellsSelect1.SellsNameClient);
            string personnerapproveid = Utils.GetFormValue(this.SellsSelect1.SellsIDClient);
            #endregion

            #region 表单验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(fileid))
            {
                msg += "-请选择申请人员！";
            }
            if (string.IsNullOrEmpty(personnelapprovename))
            {
                msg += "-请选择审批人员！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
            }
            #endregion
            #region 实体赋值
            MGovFilePersonnel model = new MGovFilePersonnel();
            model.ApplicationTime = Utils.GetDateTimeNullable(applicationtime);
            model.ApproveState = ApprovalStatus.待审核;
            model.DepartureTime = null;
            model.FileId = fileid;
            model.FileName = filename;
            model.GovPersonnelApproveList = GetApproveList(personnerapproveid, personnelapprovename, KeyId);
            model.Id = KeyId;
            model.IssueTime = DateTime.Now;
            model.OperatorID = this.SiteUserInfo.UserId;
            model.Reason = reason;
            model.StaffStatus = StaffStatus.在职;
            #endregion
            #region 提交保存
            BGovFilePersonnel BLL = new BGovFilePersonnel();
            if (doType == "add")
            {
                result = BLL.AddGovFilePersonnel(model);
                msg = result ? "添加成功！" : "添加失败";
            }
            if (doType == "update")
            {
                result = BLL.UpdateGovFilePersonnel(model);
                msg = result ? "修改成功！" : "修改失败！";
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }


        /// <summary>
        /// 获取审批人集合
        /// </summary>
        /// <param name="ids">审批人编号</param>
        /// <param name="names">审批人姓名</param>
        /// <param name="keyId">主键编号</param>
        /// <returns></returns>
        protected IList<MGovPersonnelApprove> GetApproveList(string ids, string names, string keyId)
        {
            IList<MGovPersonnelApprove> lst = new List<MGovPersonnelApprove>();
            if (!string.IsNullOrEmpty(ids) && !string.IsNullOrEmpty(names))
            {
                string[] arryIds = ids.Split(',');
                string[] arryNames = names.Split(',');
                for (int i = 0; i < arryIds.Length; i++)
                {
                    MGovPersonnelApprove model = new MGovPersonnelApprove();
                    model.ApproveID = arryIds[i];
                    model.ApproveName = arryNames[i];
                    model.ApproveState = ApprovalStatus.待审核;
                    model.ApproveTime = null;
                    model.Id = keyId;
                    lst.Add(model);
                }
                return lst;
            }
            return null;
        }


        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "add")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_新增, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_员工离职_修改, false);
                    }
                }
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