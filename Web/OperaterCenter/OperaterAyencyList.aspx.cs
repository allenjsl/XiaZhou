using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.PlanStructure;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调中心-地接,出境-地接安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-15
    /// </summary>
    public partial class OperaterAyencyList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式
        protected string panyMent = string.Empty;
        //状态
        protected string states = string.Empty;
        //确认单
        protected string querenUrl = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        /// <summary>
        /// 安排权限
        /// </summary>
        bool Privs_AnPai = false;
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourId");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitPrivs();

            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.地接确认单);

            string doType = Utils.GetQueryStringValue("action");
            switch (doType)
            {
                case "save": PageSave(); break;
                case "delete": DeleteAyency(); break;
                case "update": GetAyencyModel(); break;
                default: break;
            }

            DataInit();
        }


        #region private members
        /// <summary>
        /// 绑定安排的地接计调项
        /// </summary>
        /// <param name="tourId">团号</param>
        void DataInit()
        {
            ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(TourId, SiteUserInfo.UserId);
            if (ListPower) ListPower = panView.Visible = Privs_AnPai;

            IList<EyouSoft.Model.PlanStructure.MPlan> AyencyList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, TourId);
            if (AyencyList != null && AyencyList.Count > 0)
            {
                this.repAycentylist.DataSource = AyencyList;
                this.repAycentylist.DataBind();
            }
            else
            {
                this.phdShowList.Visible = false;
            }
        }

        /// <summary>
        /// 获取地接实体
        /// </summary>
        /// <param name="ID">计调项id</param>
        void GetAyencyModel()
        {
            string PlanId = Utils.GetQueryStringValue("PlanId");
            if (string.IsNullOrEmpty(PlanId)) RCWE("异常请求");

            EyouSoft.Model.PlanStructure.MPlanBaseInfo AyencyM = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, PlanId);

            if (AyencyM == null) RCWE("异常请求");

            this.supplierControl1.HideID = AyencyM.SourceId;
            this.supplierControl1.Name = AyencyM.SourceName;
            this.txtContentName.Text = AyencyM.ContactName;
            this.txtContentPhone.Text = AyencyM.ContactPhone;
            this.txtContentFax.Text = AyencyM.ContactFax;
            this.txtTravel.Text = AyencyM.ReceiveJourney;
            this.txtServerStand.Text = AyencyM.ServiceStandard;
            this.txtCustomer.Text = AyencyM.CustomerInfo;
            this.txtPeopleNumber.Text = AyencyM.Num.ToString();
            this.txtCostParticu.Text = AyencyM.CostDetail;
            this.txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(AyencyM.Confirmation);
            panyMent = ((int)AyencyM.PaymentType).ToString();
            this.txtStartTime.Text = UtilsCommons.GetDateString(AyencyM.StartDate, ProviderToDate);
            this.txtEndTime.Text = UtilsCommons.GetDateString(AyencyM.EndDate, ProviderToDate);
            this.txtguidNotes.Value = AyencyM.GuideNotes;
            this.txtOtherRemark.Text = AyencyM.Remarks;
            this.ddlProfit1.Items.FindByValue(AyencyM.IsRebate == true ? "0" : "1").Selected = true;
            states = ((int)AyencyM.Status).ToString();
        }

        /// <summary>
        /// 删除地接
        /// </summary>
        /// <returns></returns>
        void DeleteAyency()
        {
            if (!Privs_AnPai) RCWE(UtilsCommons.AjaxReturnJson("0", "没有操作权限!"));

            string planId = Utils.GetQueryStringValue("PlanId");

            if (string.IsNullOrEmpty(planId)) RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求!"));

            if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功!"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败!"));
            }
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(TourId);

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排地接);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排地接);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排地接);
                    break;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        void PageSave()
        {
            #region 表单赋值
            string msgArr = string.Empty;
            string SeterrorMsg = string.Empty;
            //地接社id
            string AyencyNameid = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //地接社名称
            string AyencyName = Utils.GetFormValue(this.supplierControl1.ClientText);
            //联系人
            string contectName = Utils.GetFormValue(this.txtContentName.UniqueID);
            //联系电话
            string contectPhone = Utils.GetFormValue(this.txtContentPhone.UniqueID);
            //联系传真
            string contectFax = Utils.GetFormValue(this.txtContentFax.UniqueID);
            //接待行程
            string Travel = Utils.GetFormValue(this.txtTravel.UniqueID);
            //服务标准
            string ServerStand = Utils.GetFormValue(this.txtServerStand.UniqueID);
            //游客信息
            string CustomerInfo = Utils.GetFormValue(this.txtCustomer.UniqueID);
            //人数
            string peopleNumber = Utils.GetFormValue(this.txtPeopleNumber.UniqueID);
            //费用明细
            string CostParticu = Utils.GetFormValue(this.txtCostParticu.UniqueID);
            //结算费用
            decimal CostAccount = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            //接团日期
            DateTime? TourStarTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtStartTime.UniqueID));
            //送团日期
            DateTime TourEndTime = Utils.GetDateTime(Utils.GetFormValue(this.txtEndTime.UniqueID));
            //导游需知
            string guidNeet = Utils.GetFormValue("txtguidNotes");
            //其它备注
            string remark = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(AyencyName) && string.IsNullOrEmpty(AyencyNameid))
            {
                msgArr += "请选择地接社名称!<br/>";
            }
            if (CostAccount <= 0)
            {
                msgArr += "请填写结算费用！<br/>";
            }
            if (string.IsNullOrEmpty(TourStarTime.ToString()))
            {
                msgArr += "请选择接团日期!<br/>";
            }
            if (string.IsNullOrEmpty(TourEndTime.ToString()))
            {
                msgArr += "请选择送团日期!<br/>";
            }
            if (msgArr != "")
            {
                SeterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");

                RCWE(SeterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlan Ayencymodel = new EyouSoft.Model.PlanStructure.MPlan();
            Ayencymodel.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            Ayencymodel.CompanyId = this.SiteUserInfo.CompanyId;
            Ayencymodel.Confirmation = CostAccount;
            Ayencymodel.PlanCost = CostAccount;
            Ayencymodel.ContactFax = contectFax;
            Ayencymodel.ContactName = contectName;
            Ayencymodel.ContactPhone = contectPhone;
            Ayencymodel.CostDetail = CostParticu;
            Ayencymodel.CustomerInfo = CustomerInfo;
            Ayencymodel.GuideNotes = guidNeet;
            Ayencymodel.IsRebate = Utils.GetFormValue(this.ddlProfit1.UniqueID) == "0" ? true : false;
            Ayencymodel.IssueTime = System.DateTime.Now;
            Ayencymodel.Num = Utils.GetInt(peopleNumber);
            Ayencymodel.PaymentType = (Payment)Utils.GetInt(Utils.GetFormValue("panyMent"));
            Ayencymodel.ReceiveJourney = Travel;
            Ayencymodel.Remarks = remark;
            Ayencymodel.SourceId = AyencyNameid;
            Ayencymodel.SourceName = AyencyName;
            Ayencymodel.Status = (PlanState)Utils.GetInt(Utils.GetFormValue("states"));
            Ayencymodel.TourId = Utils.GetQueryStringValue("tourId");
            Ayencymodel.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接;
            Ayencymodel.ServiceStandard = ServerStand;
            Ayencymodel.StartDate = TourStarTime;
            Ayencymodel.EndDate = TourEndTime;
            Ayencymodel.SueId = "";
            Ayencymodel.OperatorId = this.SiteUserInfo.UserId;
            Ayencymodel.OperatorName = this.SiteUserInfo.Name;
            Ayencymodel.DeptId = this.SiteUserInfo.DeptId;
            #endregion

            #region 提交操作
            //地接id
            string editid = Utils.GetQueryStringValue("PlanID");
            //修改
            if (editid != "" && editid != null)
            {
                Ayencymodel.PlanId = editid;
                if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(Ayencymodel) > 0)
                {
                    msgArr += "修改成功！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msgArr + "");

                }
                else
                {
                    msgArr += "修改失败！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");
                }
            }
            else //添加
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(Ayencymodel) > 0)
                {
                    msgArr += "添加成功！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msgArr + "");
                }
                else
                {
                    msgArr += "添加失败！";
                    SeterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");
                }
            }
            #endregion

            RCWE(SeterrorMsg);
        }
        #endregion
    }
}
