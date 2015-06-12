using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调中心-用餐安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-16
    /// </summary>
    public partial class OperaterDiningList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string panMent = string.Empty;
        protected string Status = string.Empty;
        //确认单
        protected string querenUrl = string.Empty;
        //登录人
        protected string UserId = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        /// <summary>
        /// 安排权限
        /// </summary>
        bool Privs_AnPai = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            UserId = this.SiteUserInfo.UserId;
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订餐确认单);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    case "delete":
                        Response.Clear();
                        Response.Write(DeleteDin());
                        Response.End();
                        break;
                    case "update":
                        GetDinModel();
                        break;
                    default: break;
                }

            }
            #endregion

            DataInit();
        }

        #region 价格组成 人员类型
        /// <summary>
        /// 人员类型
        /// </summary>
        /// <param name="selectedID">类型id</param>
        /// <returns></returns>
        protected string GetAdultType(string selectedID)
        {
            System.Text.StringBuilder sbAdultType = new System.Text.StringBuilder();
            List<EnumObj> AdultType = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType));
            if (AdultType != null && AdultType.Count > 0)
            {
                for (int i = 0; i < AdultType.Count - 1; i++)
                {
                    if (AdultType[i].Value == selectedID)
                    {
                        sbAdultType.Append("<option selected='selected' value=" + AdultType[i].Value + ">" + AdultType[i].Text + "</option>");
                    }
                    else
                    {
                        sbAdultType.Append("<option value=" + AdultType[i].Value + ">" + AdultType[i].Text + "</option>");
                    }
                }
            }
            return sbAdultType.ToString();
        }
        #endregion

        #region 绑定已安排的用餐列表
        /// <summary>
        /// 绑定已安排的用餐列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");            
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> list = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, null,  EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (list != null && list.Count > 0)
                {
                    this.repDinnList.DataSource = list;
                    this.repDinnList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }
        }
        #endregion

        #region 删除用餐
        /// <summary>
        /// 删除用餐
        /// </summary>
        /// <param name="planID">计调项id</param>
        /// <returns></returns>
        protected string DeleteDin()
        {
            string planId = Utils.GetQueryStringValue("planId");
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
                {                    
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功!");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败!");
                }
            }
            return msg;
        }
        #endregion

        #region 获取用餐实体
        /// <summary>
        /// 获取用餐实体
        /// </summary>
        /// <param name="planID">计调项id</param>
        protected void GetDinModel()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, planId);
                if (baseinfo != null)
                {
                    this.supplierControl1.HideID = baseinfo.SourceId;
                    this.supplierControl1.Name = baseinfo.SourceName;
                    this.txtContactFax.Text = baseinfo.ContactFax;
                    this.txtContactName.Text = baseinfo.ContactName;
                    this.txtContactPhone.Text = baseinfo.ContactPhone;
                    this.txtDinningDate.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    if (baseinfo.PlanDiningPricelist != null && baseinfo.PlanDiningPricelist.Count > 0)
                    {
                        this.tabViewPrices.Visible = false;
                        this.repPriceList.DataSource = baseinfo.PlanDiningPricelist;
                        this.repPriceList.DataBind();
                    }
                    else
                    {
                        this.tabViewPrices.Visible = true;
                    }
                    this.txtPeopleNum.Text = baseinfo.Num.ToString();
                    this.txtCostDetail.Text = baseinfo.CostDetail;
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherMarks.Text = baseinfo.Remarks;
                    panMent = ((int)baseinfo.PaymentType).ToString();
                    Status = ((int)baseinfo.Status).ToString();
                }
                else
                {
                    Utils.ResponseGoBack();
                }
            }
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(Utils.GetQueryStringValue("tourid"));

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用餐))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用餐, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用餐);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用餐))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用餐, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用餐);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用餐))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用餐, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用餐);
                    break;
            }
        }
        #endregion


        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected string PageSave()
        {
            #region 表单取值
            string msg = string.Empty;
            string setErrorMsg = string.Empty;
            //餐馆名称
            string SourceName = Utils.GetFormValue(this.supplierControl1.ClientText);
            string SourceID = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //联系人 电话 传真
            string ContactName = Utils.GetFormValue(this.txtContactName.UniqueID);
            string ContactPhone = Utils.GetFormValue(this.txtContactPhone.UniqueID);
            string ContactFax = Utils.GetFormValue(this.txtContactFax.UniqueID);
            //用餐时间
            DateTime? Dintime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtDinningDate.UniqueID));
            //价格组成
            //人员类型
            string[] AdultType = Utils.GetFormValues("selAdultType");
            //早 次数 人数 价格 
            string[] FrequencyB = Utils.GetFormValues("txtFrequencyB");
            string[] PeopleNumsB = Utils.GetFormValues("txtPeopleNumsB");
            string[] PricesB = Utils.GetFormValues("txtPricesB");
            //中 次数 人数 价格
            string[] FrequencyL = Utils.GetFormValues("txtFrequencyL");
            string[] PeopleNumsL = Utils.GetFormValues("txtPeopleNumsL");
            string[] PricesL = Utils.GetFormValues("txtPricesL");
            //晚 次数 人数 价格
            string[] FrequencyS = Utils.GetFormValues("txtFrequencyS");
            string[] PeopleNumsS = Utils.GetFormValues("txtPeopleNumsS");
            string[] PricesS = Utils.GetFormValues("txtPricesS");
            //费用明细 结算费用
            string CostDetail = Utils.GetFormValue(this.txtCostDetail.UniqueID);
            decimal totalprices = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            //导游须知 其它备注
            string GuidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherMark = Utils.GetFormValue(this.txtOtherMarks.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(SourceID) && string.IsNullOrEmpty(SourceName))
            {
                msg += "请选择餐馆!<br/>";
            }
            if (totalprices <= 0)
            {
                msg += "请输入结算费用！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selPanyMent")))
            {
                msg += "请选择支付方式!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStatus")))
            {
                msg += "请选择状态!<br/>";
            }
            if (msg != "")
            {
                setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return setErrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalprices;
            baseinfo.PlanCost = totalprices;
            baseinfo.ContactFax = ContactFax;
            baseinfo.ContactName = ContactName;
            baseinfo.ContactPhone = ContactPhone;
            baseinfo.CostDetail = CostDetail;
            baseinfo.GuideNotes = GuidNotes;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("selPanyMent"));
            int nums = 0;
            baseinfo.PlanDiningPricelist = new List<EyouSoft.Model.PlanStructure.MPlanDiningPrice>();
            for (int i = 0; i < FrequencyB.Length; i++)
            {
                EyouSoft.Model.PlanStructure.MPlanDiningPrice prices = new EyouSoft.Model.PlanStructure.MPlanDiningPrice();
                prices.PeopleB = Utils.GetInt(PeopleNumsB[i]);
                prices.PriceB = Utils.GetDecimal(PricesB[i]);
                prices.TimeB = Utils.GetInt(FrequencyB[i]);
                prices.PriceL = Utils.GetDecimal((PricesL[i]));
                prices.TimeL = Utils.GetInt(FrequencyL[i]);
                prices.PeopleL = Utils.GetInt(PeopleNumsL[i]);
                prices.PeopleS = Utils.GetInt(PeopleNumsS[i]);
                prices.TimeS = Utils.GetInt(FrequencyS[i]);
                prices.PriceS = Utils.GetDecimal(PricesS[i]);
                prices.Pricetyp = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType), AdultType[i]);
                nums += Utils.GetInt(PeopleNumsB[i]) + Utils.GetInt(PeopleNumsL[i]) + Utils.GetInt(PeopleNumsS[i]);
                baseinfo.PlanDiningPricelist.Add(prices);
            }
            baseinfo.Num = Utils.GetInt(Utils.GetFormValue(this.txtPeopleNum.UniqueID));
            baseinfo.Remarks = otherMark;
            baseinfo.SourceId = SourceID; baseinfo.SourceName = SourceName;
            baseinfo.StartDate = Dintime;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            baseinfo.SueId = "";
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("selPanyMent"));
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            #endregion

            #region 提交操作
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                baseinfo.PlanId = planId;
                if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo) > 0)
                {
                    msg += "修改成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "修改失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo) > 0)
                {
                    msg += "添加成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "添加失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return setErrorMsg;
        }
        #endregion
    }
}
