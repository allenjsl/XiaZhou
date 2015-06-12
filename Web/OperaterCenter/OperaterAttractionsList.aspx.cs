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
    /// 计调中心 -出境,地接-景点安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-13
    /// </summary>
    public partial class OperaterAttractionsList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string panyMent = string.Empty;
        protected string Status = string.Empty;
        //景点名称id
        protected System.Text.StringBuilder SpotNameList = new System.Text.StringBuilder();
        //景点确认单
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
        protected string SourceID = string.Empty;
        //预控编号
        protected string SueID = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            UserId = this.SiteUserInfo.UserId;
            if (new EyouSoft.BLL.SysStructure.BSys().IsExistsMenu2(this.SiteUserInfo.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源预控_景点预控))
            {
                this.supplierControl1.Flag = 1;
                this.supplierControl1.TourID = Utils.GetQueryStringValue("TourId");
            }
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.景点确认单);

            string doType = Utils.GetQueryStringValue("action");

            switch (doType)
            {
                case "save": PageSave(); break;
                case "delete": DeleteSpot(); break;
                case "update": GetModelSpot(); break;
                default:
                    //this.txtLookAbout1.Text = UtilsCommons.GetDateString(DateTime.Now, this.ProviderToDate);
                    //this.txtLookAbout2.Text = UtilsCommons.GetDateString(DateTime.Now, this.ProviderToDate);
                    break;
            }  

            PageInit();
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void PageInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");                  
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> SpotList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (SpotList != null && SpotList.Count > 0)
                {
                    this.repSpotList.DataSource = SpotList;
                    this.repSpotList.DataBind();
                }
                else {
                    this.phdShowList.Visible = false;
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
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排景点))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排景点, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排景点);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排景点))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排景点, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排景点);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排景点))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排景点, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排景点);
                    break;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="SpotID"></param>
        /// <returns></returns>
        void DeleteSpot()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }

            RCWE(msg);
        }
        #endregion

        #region 获取实体
        /// <summary>
        /// 获取实体
        /// </summary>
        protected void GetModelSpot()
        {            
            string planId = Utils.GetQueryStringValue("PlanId");
            //初始化修改信息
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo spotModel = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, planId);
                if (spotModel != null)
                {
                    this.supplierControl1.HideID = spotModel.SourceId;
                    this.supplierControl1.Name = spotModel.SourceName;
                    SourceID = spotModel.SourceId;
                    if (!string.IsNullOrEmpty(spotModel.SueId.Trim()))
                    {
                        this.supplierControl1.HideID_zyyk = spotModel.SueId;
                        this.supplierControl1.isyukong = "1";
                        SueID = spotModel.SueId;
                        var sue = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelBySightId(spotModel.SueId, this.SiteUserInfo.CompanyId);
                        if (sue != null)
                        {
                            this.hidUserNum.Value = (sue.ControlNum - sue.AlreadyNum + spotModel.Num).ToString();
                        }
                    }
                    else
                    {
                        this.supplierControl1.isyukong = "0";
                    }

                    this.txtContectName.Text = spotModel.ContactName;
                    this.txtContectPhone.Text = spotModel.ContactPhone;
                    this.txtContectFax.Text = spotModel.ContactFax;
                    if (spotModel.PlanAttractions != null)
                    {
                        this.txtAdultNums.Text = spotModel.PlanAttractions.AdultNumber.ToString();
                        this.txtChildrenNums.Text = spotModel.PlanAttractions.ChildNumber.ToString();
                        //景点名称
                        if (spotModel.SourceId != "")
                        {
                            EyouSoft.Model.SourceStructure.MSourceSpot model = new EyouSoft.BLL.SourceStructure.BSource().GetSpotModel(spotModel.SourceId);
                            if (model != null)
                            {
                                if (model.PriceSystemList != null && model.PriceSystemList.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(SueID))
                                    {
                                        model.PriceSystemList =model.PriceSystemList.Where(m => m.Id == spotModel.PlanAttractions.AttractionsId).ToList();
                                    }
                                    for (int i = 0; i < model.PriceSystemList.Count; i++)
                                    {
                                        if (spotModel.PlanAttractions.AttractionsId == model.PriceSystemList[i].Id)
                                        {
                                            SpotNameList.Append("<option selected=\"selected\" value='" + model.PriceSystemList[i].Id + "," + model.PriceSystemList[i].SpotName + "'>" + model.PriceSystemList[i].SpotName + "</option>");
                                        }
                                        else
                                        {
                                            SpotNameList.Append("<option value='" + model.PriceSystemList[i].Id + "," + model.PriceSystemList[i].SpotName + "'>" + model.PriceSystemList[i].SpotName + "</option>");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this.txtLookAbout1.Text = UtilsCommons.GetDateString(spotModel.StartDate, ProviderToDate);
                    this.txtLookAbout2.Text = UtilsCommons.GetDateString(spotModel.EndDate, ProviderToDate);
                    this.txtScanTime1.Text = spotModel.StartTime;
                    this.txtScanTime2.Text = spotModel.EndTime;
                    this.txtCostParticu.Text = spotModel.CostDetail;
                    this.txttotalMoney.Text = Utils.FilterEndOfTheZeroDecimal(spotModel.Confirmation);
                    panyMent = ((int)spotModel.PaymentType).ToString();
                    this.txtGuidNotes.Text = spotModel.GuideNotes;
                    this.txtOtherRemarks.Text = spotModel.Remarks;
                    this.ddlProfit1.Items.FindByValue(spotModel.IsRebate == true ? "0" : "1").Selected = true;
                    Status = ((int)spotModel.Status).ToString();
                }
            }
        }
        #endregion

        #region 保存
        void PageSave()
        {
            #region 表单赋值
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            //景点公司name id
            string spotComName = Utils.GetFormValue(this.supplierControl1.ClientText);
            string spotComID = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //联系人 电话 传真
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            //景点名称 人数(成人 儿童)
            string spotName = Utils.GetFormValue("spotList").Split(',')[1];
            string adultNum = Utils.GetFormValue(this.txtAdultNums.UniqueID);
            string childrenNum = Utils.GetFormValue(this.txtChildrenNums.UniqueID);
            //游览时间
            DateTime? lookAbout1 = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtLookAbout1.UniqueID));
            string scanTime1 = Utils.GetFormValue(this.txtScanTime1.UniqueID);
            DateTime? lookAbout2 = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtLookAbout2.UniqueID));
            string scanTime2 = Utils.GetFormValue(this.txtScanTime2.UniqueID);
            //费用明细 结算费用
            string CostParticu = Utils.GetFormValue(this.txtCostParticu.UniqueID);
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txttotalMoney.UniqueID));
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherRemarks.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(spotComID) && string.IsNullOrEmpty(spotComName))
            {
                msg += "请选择景点公司！<br/>";
            }
            if (string.IsNullOrEmpty(spotName))
            {
                msg += "请选择景点名称!<br/>";
            }
            if (string.IsNullOrEmpty(adultNum))
            {
                msg += "请输入成人数！<br/>";
            }
            if (!lookAbout1.HasValue)
            {
                msg += "请输入浏览开始时间!<br>";
            }
            /*if (string.IsNullOrEmpty(scanTime1.ToString()))
            {
                msg += "请输入浏览时间!<br/>";
            }*/
            /*if (string.IsNullOrEmpty(lookAbout2.ToString()))
            {
                msg += "请输入浏览时间！<br/>";
            }*/
            /*if (string.IsNullOrEmpty(scanTime2.ToString()))
            {
                msg += "请输入浏览时间！<br/>";
            }*/
            if (totalMoney <= 0)
            {
                msg += "请输入结算费用！<br/>";
            }            
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStatus")))
            {
                msg += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue(this.ddlProfit1.UniqueID)))
            {
                msg += "请选择是否返利！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                RCWE(seterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalMoney;
            baseinfo.PlanCost = totalMoney;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.EndDate = lookAbout2;
            baseinfo.EndTime = scanTime2;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IsRebate = Utils.GetFormValue(this.ddlProfit1.UniqueID) == "0" ? true : false;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Num = Utils.GetInt(adultNum) + Utils.GetInt(childrenNum);
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPanyMent"));
            baseinfo.Remarks = otherRemark;
            baseinfo.SourceId = spotComID;
            baseinfo.SourceName = spotComName;
            baseinfo.StartDate = lookAbout1;
            baseinfo.StartTime = scanTime1;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            if (Utils.GetFormValue(this.supplierControl1.ClientIsyukong) == "1")
            {
                baseinfo.SueId = Utils.GetFormValue(this.supplierControl1.ClientzyykValue);
            }
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点;
            baseinfo.PlanAttractions = new EyouSoft.Model.PlanStructure.MPlanAttractions();
            baseinfo.PlanAttractions.ChildNumber = Utils.GetInt(childrenNum);
            baseinfo.PlanAttractions.AdultNumber = Utils.GetInt(adultNum);
            baseinfo.PlanAttractions.Attractions = spotName;
            baseinfo.PlanAttractions.AttractionsId = Utils.GetFormValue("spotList").Split(',')[0];
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            baseinfo.CostDetail = CostParticu;
            #endregion

            #region 表单提交
            //景点id
            string editid = Utils.GetQueryStringValue("PlanId");
            if (editid != "" && editid != null) //修改
            {
                baseinfo.PlanAttractions.PlanId = editid;
                baseinfo.PlanId = editid;
                var r = new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo);
                if ( r== 1)
                {
                    msg += "修改成功！";                    
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if(r==0)
                {
                    msg += "修改失败！";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }                                
                else if (r == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else //添加
            {
                var r = new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo);
                if ( r==1)
                {
                    msg += "添加成功！";                    
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if(r==0)
                {
                    msg += "添加失败！";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (r == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }

            RCWE(seterrorMsg);

            #endregion
        }
        #endregion

    }
}
