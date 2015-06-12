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
    /// 计调中心-出境,地接-导游安排
    /// 创建时间：2011-09-16
    /// 创建人:李晓欢
    /// </summary>
    public partial class OperaterGuiderList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式
        protected string panyMent = string.Empty;
        //状态
        protected string states = string.Empty;
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
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            PowerControl();

            UserId = this.SiteUserInfo.UserId;
            GetGuidJobHtml();
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.导游任务单);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            //存在ajax请求
            switch (doType)
            {
                case "save": PageSave(); break;
                case "delete": deleteGuid(); break;
                case "update":GetGuidModel();break;
                default: InitDaoYouXuZhi(); break;
            }               
            #endregion

            DataInit();
        }


        #region 页面初始化
        /// <summary>
        /// 团队计调项
        /// </summary>
        protected void DataInit()
        {
            ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(TourId, SiteUserInfo.UserId);
            if (ListPower) ListPower = panView.Visible = Privs_AnPai;

            IList<EyouSoft.Model.PlanStructure.MPlan> guideList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, TourId);
            if (guideList != null && guideList.Count > 0)
            {
                this.repGuidList.DataSource = guideList;
                this.repGuidList.DataBind();
            }
            else
            {
                this.phdShowList.Visible = false;
            }
        }
        #endregion


        #region 权限判断
        protected void PowerControl()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(TourId);

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排导游))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排导游, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排导游);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排导游))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排导游, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排导游);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排导游))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排导游, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排导游);
                    break;
            }
        }
        #endregion

        #region 绑定导游任务类型
        /// <summary>
        /// 绑定导游任务类型
        /// </summary>
        /// <param name="selected">类型id</param>
        /// <returns></returns>
        protected void GetGuidJobHtml()
        {
             List<EnumObj> Guidjob = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType));
             if (Guidjob != null && Guidjob.Count > 0)
             {
                 for (int i = 0; i < Guidjob.Count; i++)
                 {
                     ListItem item = new ListItem();
                     item.Text = Guidjob[i].Text;
                     item.Value = Guidjob[i].Value;
                     this.guidType.Items.Add(item);
                 }
             }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除导游
        /// </summary>
        /// <param name="ID">计调项id</param>
        /// <returns></returns>
        void deleteGuid()
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

            RCWE(msg);
        }
        #endregion

        #region 获取实体 
        /// <summary>
        /// 获取导游实体
        /// </summary>
        /// <param name="ID">计调项id</param>
        protected void GetGuidModel()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, planId);
                if (baseinfo != null)
                {                    
                    this.GuidControl1.GuidID = baseinfo.SourceId;
                    this.GuidControl1.GuidName = baseinfo.SourceName;
                    this.txtguidPhone.Text = baseinfo.ContactPhone;
                    if (baseinfo.PlanGuide != null)
                    {
                        this.txtGroupPlace.Text = baseinfo.PlanGuide.OnLocation;
                        this.txtUnderPlace.Text = baseinfo.PlanGuide.NextLocation;                        
                        this.guidType.Items.FindByValue(((int)baseinfo.PlanGuide.TaskType).ToString()).Selected = true;
                    }
                    this.txtGroupTime.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    this.txtunderTime.Text = UtilsCommons.GetDateString(baseinfo.EndDate, ProviderToDate);
                    this.txttravel.Text = baseinfo.ReceiveJourney;
                    this.txtserverStand.Text = baseinfo.ServiceStandard;
                    //this.txtOtherRemark.Text = baseinfo.Remarks;
                    this.txtCostParticu.Text = baseinfo.CostDetail.ToString();
                    this.txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    states = ((int)baseinfo.Status).ToString();
                    txtDaoYouXuZhi.Text = baseinfo.GuideNotes;
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        void PageSave()
        {
            #region 表单赋值
            string seterrorMsg = string.Empty;
            string msg = string.Empty;
            //导游姓名
            string guidName = Utils.GetFormValue(this.GuidControl1.GuidNameClient);
            //导游id
            string guidID = Utils.GetFormValue(this.GuidControl1.GuidIDClient);          
            //导游电话
            string guidPhone = Utils.GetFormValue(this.txtguidPhone.UniqueID);
            //上团地点
            string GroupPlace = Utils.GetFormValue(this.txtGroupPlace.UniqueID);            
            //上团时间
            DateTime groupTime = Utils.GetDateTime(Utils.GetFormValue(this.txtGroupTime.UniqueID));          
            //下团地点
            string UnderPlace = Utils.GetFormValue(this.txtUnderPlace.UniqueID);            
            //下团时间
            DateTime UnderPlaceTime = Utils.GetDateTime(Utils.GetFormValue(this.txtunderTime.UniqueID));
            //任务类型
            string guidJobType = Utils.GetFormValue(this.guidType.UniqueID);
            //接待行程
            string treval = Utils.GetFormValue(this.txttravel.UniqueID);
            //服务标准
            string serverStande = Utils.GetFormValue(this.txtserverStand.UniqueID);
            //其它备注
            //string remark = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            //费用明细
            string CostParticu = Utils.GetFormValue(this.txtCostParticu.UniqueID);
            //结算费用
            decimal CostAccount = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            string daoYouXuZhi = Utils.GetFormValue(txtDaoYouXuZhi.UniqueID);
            #endregion 

            #region 后台验证
            if (string.IsNullOrEmpty(guidID) && string.IsNullOrEmpty(guidName))
            {
                msg += "请选择导游!<br/>";
            }
            if (string.IsNullOrEmpty(GroupPlace))
            {
                msg += "请输入上团地点!<br/>";
            }
            if (string.IsNullOrEmpty(groupTime.ToString()))
            {
                msg += "请选择上团时间!<br/>";
            }
            if (string.IsNullOrEmpty(UnderPlace))
            {
                msg += "请输入下团地点!<br/>";
            }
            if (string.IsNullOrEmpty(UnderPlaceTime.ToString()))
            {
                msg += "请选择下团时间!<br/>";
            }
            if (string.IsNullOrEmpty(guidJobType))
            {
                msg += "请选择导游任务类型!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("status")))
            {
                msg += "请选择计调状态！<br/>";
            }
            if (CostAccount <= 0)
            {
                msg += "请填写结算费用！<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                RCWE(seterrorMsg);
            }
            #endregion 

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseInfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseInfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseInfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseInfo.Confirmation = CostAccount;
            baseInfo.PlanCost = CostAccount;
            baseInfo.ContactPhone = guidPhone;
            baseInfo.CostDetail = CostParticu;
            baseInfo.IssueTime = System.DateTime.Now;
            baseInfo.Num = new TimeSpan(UnderPlaceTime.Ticks).Subtract(new TimeSpan(groupTime.Ticks)).Duration().Days;
            baseInfo.PlanGuide = new EyouSoft.Model.PlanStructure.MPlanGuide();
            baseInfo.PlanGuide.NextLocation = UnderPlace;
            baseInfo.PlanGuide.OnLocation = GroupPlace;
            baseInfo.PlanGuide.TaskType = (EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanGuideTaskType), guidJobType);
            baseInfo.SourceName = guidName;
            baseInfo.SourceId = guidID;
            baseInfo.ReceiveJourney = treval;
            //baseInfo.Remarks = remark;
            baseInfo.GuideNotes = daoYouXuZhi;
            baseInfo.ServiceStandard = serverStande;
            baseInfo.StartDate = groupTime;
            baseInfo.EndDate = UnderPlaceTime;
            baseInfo.TourId = TourId;
            baseInfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游;
            baseInfo.SueId = "";
            baseInfo.OperatorId = this.SiteUserInfo.UserId;
            baseInfo.OperatorName = this.SiteUserInfo.Name;
            baseInfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("status"));
            baseInfo.DeptId = this.SiteUserInfo.DeptId;
            baseInfo.PaymentType = EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付;
            #endregion

            #region 提交操作
            //导游id
            string editid = Utils.GetQueryStringValue("planId");
            //修改
            if (editid != "" && editid != null)
            {
                baseInfo.PlanId = editid;
                baseInfo.PlanGuide.PlanId = editid;
                if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseInfo) > 0)
                {
                    msg += "修改成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");                  
                }                                               
            }
            else //添加
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseInfo) > 0)
                {
                    msg += "添加成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }

            #endregion 
            RCWE(seterrorMsg);
        }
        #endregion

        /// <summary>
        /// init 导游须知
        /// </summary>
        void InitDaoYouXuZhi()
        {
            var info = new EyouSoft.BLL.PlanStructure.BPlan().GetGuidePrint(TourId);

            if (info == null) return;

            txtDaoYouXuZhi.Text = info.GuideNotes;
        }
    }
}
