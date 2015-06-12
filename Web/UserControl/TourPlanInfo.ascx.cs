using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    /// <summary>
    /// 团队计划派团给计调，计调安排头部显示计划的基本信息
    /// 创建人：李晓欢
    /// 创建时间:2011-09-28
    /// </summary>
    public partial class TourPlanInfo : System.Web.UI.UserControl
    {
        //安排类型
        private EyouSoft.Model.EnumType.PlanStructure.PlanProject project;
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject Project
        {
            get { return project; }
            set { project = value; }
        }

        //系统公司编号
        private string _companyId;
        public string CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                if (Utils.GetQueryStringValue("type") == "Agency" || Utils.GetQueryStringValue("type") == "Departure" || Utils.GetQueryStringValue("type") == "Team")
                {
                    string tourId = Utils.GetQueryStringValue("Id");
                    if (tourId != "" && !string.IsNullOrEmpty(tourId))
                    {                      
                        EyouSoft.Model.PlanStructure.MPlanTourGuide tourGuidInfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(tourId, CompanyId, Project);
                        if (tourGuidInfo != null)
                        {
                            //线路名称
                            this.litRouteName.Text = tourGuidInfo.RouteName;
                            //团号
                            this.litTourId.Text = tourGuidInfo.TourCode;
                            //出发时间
                            this.litTourendInfo.Text = tourGuidInfo.LDate.ToString("yyyy-MM-dd");
                            //出发交通
                            this.litTourStartInfo.Text = tourGuidInfo.LTraffic;
                            //销售员
                            if (tourGuidInfo.SaleInfo != null)
                            {
                                this.LitSellerName.Text = tourGuidInfo.SaleInfo.Name;
                            }
                            //人数
                            int Count = tourGuidInfo.Adults + tourGuidInfo.Childs + tourGuidInfo.Others;
                            this.LitPeople.Text = Count.ToString();
                            //内部信息
                            if (tourGuidInfo.TourService != null)
                            {
                                this.LitInterInfo.Text = tourGuidInfo.TourService.InsiderInfor;
                            }
                            //计调需知
                            this.LitOperNeet.Text = tourGuidInfo.ServiceStandards;
                        }
                    }
                }
            }
        }
    }
}