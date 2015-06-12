using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.QualityCenter.Complaint
{
    /// <summary>
    /// 质量管理-投诉管理-投诉处理
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-11
    public partial class CompStatistic : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("ComplaintsId");
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

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void PageInit(string id)
        {
            EyouSoft.BLL.CrmStructure.BCrmComplaint BLL = new EyouSoft.BLL.CrmStructure.BCrmComplaint();
            EyouSoft.Model.CrmStructure.MCrmComplaint model = BLL.GetCrmComplaintModel(id);
            if (model != null)
            {
                this.hidComplaintsId.Value = model.ComplaintsId;

                this.lbTourCode.Text = model.TourCode;

                this.lbComplaintsName.Text = model.ComplaintsName;

                this.lbComplaintsTime.Text = string.Format("{0:yyyy-MM-dd}", model.ComplaintsTime);

                this.lbComplaintsIdentity.Text = model.ComplaintsIdentity;

                this.lbComplaintsTel.Text = model.ComplaintsTel;

                this.lbComplaintsType.Text = model.ComplaintsType;

                this.lbComplaintsOpinion.Text = model.ComplaintsOpinion;

                this.lbHandleName.Text = model.HandleName;
                this.txtHandleName.Text = model.HandleName;

                this.lbHandleTime.Text = model.HandleTime.HasValue ? model.HandleTime.Value.ToString("yyyy-MM-dd") : "";
                this.txtHandleTime.Text = model.HandleTime.HasValue ? model.HandleTime.Value.ToString("yyyy-MM-dd") : "";

                this.lbHandleResult.Text = model.HandleOpinion;
                this.txtHandleResult.Text = model.HandleOpinion;
                if (model.IsHandle)
                {
                    handle.Checked = true;
                }
                else
                {
                    nohandle.Checked = true;
                }
                this.lbIsHandle.Text = model.IsHandle ? "是" : "否";
            }
        }

        private void PageSave(string doType)
        {
            #region 表单取值
            string complaintsid = Utils.GetFormValue(hidComplaintsId.UniqueID);
            string handlename = Utils.GetFormValue(txtHandleName.UniqueID);
            string handletime = Utils.GetFormValue(txtHandleTime.UniqueID);
            string handleresult = Utils.GetFormValue(txtHandleResult.UniqueID);
            bool ishandle = Utils.GetFormValue("rbIsHandle") == "handle" ? true : false; ;
            #endregion
            #region 数据验证
            string msg = "";
            if (string.IsNullOrEmpty(handlename))
            {
                msg += "-请输入处理人！<br/>";
            }
            if (string.IsNullOrEmpty(handleresult))
            {
                msg += "-请输入处理意见！<br/>";
            }
            if (string.IsNullOrEmpty(handletime))
            {
                msg += "-请输入处理时间！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }

            #endregion
            #region 实体赋值
            EyouSoft.BLL.CrmStructure.BCrmComplaint BLL = new EyouSoft.BLL.CrmStructure.BCrmComplaint();
            #endregion
            #region 保存提交
            bool flag = BLL.SetComplaintDeal(complaintsid, handlename, Utils.GetDateTime(handletime), handleresult, ishandle);
            string result = flag ? "1" : "0";
            msg = flag ? "投诉处理成功！" : "投诉处理失败！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result, msg));
            Response.End();
            #endregion
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_栏目, false);
            }
            else
            {
                this.ph_save.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_投诉管理_登记);
            }
        }
        #endregion
    }
}