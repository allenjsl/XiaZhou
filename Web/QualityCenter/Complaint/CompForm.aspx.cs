using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.Model.CrmStructure;
using System.Text;
namespace Web.QualityCenter.Complaint
{
    /// <summary>
    /// 质量管理-投诉管理-投诉添加表单
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-19
    public partial class CompForm : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            string save = Utils.GetQueryStringValue("save");
            //存在ajax请求
            if (save == "save")
            {
                PageSave();
            }
            #endregion
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            #region 表单取值
            //团队编号
            string tourno = Utils.GetFormValue("txt_oneControl");
            string tourid = Utils.GetFormValue("txt_TourId");
            //投诉人
            string complaintor = Utils.GetFormValue(txtComplaintor.UniqueID);
            //投诉时间
            string complaintime = Utils.GetFormValue(txtComplaintime.UniqueID);
            //投诉人类型
            string complaintortype = Utils.GetFormValue(txtComplaintorType.UniqueID);
            //投诉人电话
            string complaintel = Utils.GetFormValue(txtComplainTel.UniqueID);
            //投诉类型
            string complaintype = Utils.GetFormValue(txtComplainType.UniqueID);
            //投诉已经
            string complainremark = Utils.GetFormValue(txtComplainRemark.UniqueID);
            #endregion
            #region 表单验证
            string msg = "";
            if (string.IsNullOrEmpty(tourno) && string.IsNullOrEmpty(tourid))
            {
                msg += "请选择团号！<br/>";
            }
            if (string.IsNullOrEmpty(complaintor))
            {
                msg += "请输入投诉人！<br/>";
            }
            if (string.IsNullOrEmpty(complaintime))
            {
                msg += "请输入投诉时间！<br/>";
            }
            if (string.IsNullOrEmpty(complaintype))
            {
                msg += "请输入投诉类型！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            #endregion
            #region 实体赋值
            MCrmComplaint model = new MCrmComplaint();
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.ComplaintsIdentity = complaintortype;
            model.ComplaintsName = complaintor;
            model.ComplaintsOpinion = complainremark;
            model.ComplaintsTel = complaintel;
            model.ComplaintsTime = Utils.GetDateTimeNullable(complaintime);
            model.ComplaintsType = complaintype;
            model.HandleName = "";
            model.IssueTime = DateTime.Now;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.TourCode = tourno;
            model.TourId = tourid;
            #endregion
            #region 提交保存
            EyouSoft.BLL.CrmStructure.BCrmComplaint BLL = new EyouSoft.BLL.CrmStructure.BCrmComplaint();
            int flag = BLL.AddCrmComplaintModel(model, tourno);
            bool result = flag > 0 ? true : false;
            msg = result ? "投诉成功！" : "投诉失败！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(flag.ToString(), msg));
            Response.End();
            #endregion
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_登记))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_登记, false);
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