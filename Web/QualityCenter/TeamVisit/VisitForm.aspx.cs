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
using EyouSoft.BLL.CrmStructure;
using System.Text;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.EnumType.CrmStructure;
namespace Web.QualityCenter.TeamVisit
{
    /// <summary>
    /// 质量管理-团队回访-回访表单
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-11
    public partial class VisitForm : BackPage
    {
        protected int VisitListCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            string save = Utils.GetQueryStringValue("save");
            string tourId = Utils.GetQueryStringValue("tourId");
            string visitId = Utils.GetQueryStringValue("visitId");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(visitId, tourId);
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string visitId, string tourId)
        {
            #region 下拉控件初始化
            this.visitType.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.CrmReturnType));
            this.visitType.DataTextField = "Text";
            this.visitType.DataValueField = "Value";
            this.visitType.DataBind();

            this.beVisitType.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.CrmStructure.CrmIdentity));
            this.beVisitType.DataTextField = "Text";
            this.beVisitType.DataValueField = "Value";
            this.beVisitType.DataBind();
            #endregion

            string visittype = string.Empty;
            string bevisittype = string.Empty;

            BCrmVisit BLL = new BCrmVisit();
            MCrmVisit model = BLL.GetVisitModel(tourId, visitId);
            if (model != null)
            {
                //回访编号
                this.hidKeyId.Value = visitId;
                this.hidTourId.Value = tourId;
                //回访类型
                if (model.ReturnType != null)
                {
                    visittype = Convert.ToString((int)model.ReturnType);
                    this.lbVisitType.Text = model.ReturnType.ToString();
                }
                //被访人身份
                if (model.Identity != null)
                {
                    bevisittype = Convert.ToString((int)model.Identity);
                    this.lbBeVisitType.Text = model.Identity.ToString();
                }
                //被访人
                this.txtVisitor.Text = model.Name;
                this.lbVisitor.Text = model.Name;
                //被访人电话
                this.txtVisitorTel.Text = model.Telephone;
                this.lbVisitorTel.Text = model.Telephone;
                //计调安排项
                if (model.VisitDetailList != null && model.VisitDetailList.Count > 0)
                {
                    this.rpt_VisitDetail.DataSource = model.VisitDetailList;
                    this.rpt_VisitDetail.DataBind();
                    this.VisitListCount = model.VisitDetailList.Count;
                }
                //团队均分
                this.txtQualityScore.Text = model.QualityScore.ToString() == "0" ? "" : model.QualityScore.ToString();
                this.lbQualityScore.Text = model.QualityScore.ToString();
                //总评
                this.txtTotal.Text = model.Total;
                this.lbTotal.Text = model.Total;
            }
            else
            {
                this.hidTourId.Value = tourId;

            }
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            //回访编号
            string visitid = Utils.GetFormValue(hidKeyId.UniqueID);
            string tourid = Utils.GetFormValue(hidTourId.UniqueID);
            //回访类型
            string visittype = Utils.GetFormValue(visitType.UniqueID);
            //被访人身份
            string bevisittype = Utils.GetFormValue(beVisitType.UniqueID);
            //被访人
            string bevisitor = Utils.GetFormValue(txtVisitor.UniqueID);
            //被访人电话
            string bevisitorTel = Utils.GetFormValue(txtVisitorTel.UniqueID);
            //计调安排项
            string[] planType = Utils.GetFormValues("planType");
            string[] sourceName = Utils.GetFormValues("sourceName");
            string[] planId = Utils.GetFormValues("planId");
            string[] sourceId = Utils.GetFormValues("sourceId");
            string[] txtScore = Utils.GetFormValues("txtScore");
            string[] txtTotalDesc = Utils.GetFormValues("txtTotalDesc");
            IList<MCrmVisitDetail> list = new List<MCrmVisitDetail>();
            for (int i = 0; i < planType.Length; i++)
            {
                MCrmVisitDetail model = new MCrmVisitDetail();
                model.PlanType = (PlanProject)Utils.GetInt(planType[i]);
                model.SourceName = sourceName[i];
                model.SourceId = sourceId[i];
                model.PlanId = planId[i];
                model.Score = (float)Utils.GetDecimal(txtScore[i]);
                model.TotalDesc = txtTotalDesc[i];
                list.Add(model);
            }
            //团队均分
            string txtqualityscore = Utils.GetFormValue(txtQualityScore.UniqueID);
            //总评
            string txttotal = Utils.GetFormValue(txtTotal.UniqueID);
            #endregion
            #region 数据验证

            #endregion
            #region 实体赋值
            MCrmVisit visitModel = new MCrmVisit();
            visitModel.CompanyId = this.SiteUserInfo.CompanyId;
            if (!string.IsNullOrEmpty(bevisittype))
            {
                visitModel.Identity = (CrmIdentity)Utils.GetInt(bevisittype);
            }

            visitModel.IssueTime = DateTime.Now;
            visitModel.Name = bevisitor;
            visitModel.OperatorId = this.SiteUserInfo.UserId;
            visitModel.QualityScore = (float)Utils.GetDecimal(txtqualityscore);
            if (!string.IsNullOrEmpty(visittype))
            {
                visitModel.ReturnType = (CrmReturnType)Utils.GetInt(visittype);
            }
            visitModel.Telephone = bevisitorTel;
            visitModel.Total = txttotal;
            visitModel.TourId = tourid;
            visitModel.VisitId = visitid;
            visitModel.VisitDetailList = list;
            #endregion
            #region 提交保存
            BCrmVisit BLL = new BCrmVisit();
            int flag = 0;
            string msg = "";
            if (!string.IsNullOrEmpty(doType))
            {
                flag = BLL.AddCrmVisitModel(visitModel);
                msg = flag > 0 ? "团队回访成功！" : "团队回访失败！";
            }

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
            if (Utils.GetQueryStringValue("doType") == "add")
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.质量管理_团队回访_登记))
                {
                    ph_save.Visible = false;
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