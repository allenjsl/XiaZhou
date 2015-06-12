using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SystemSet
{
    /// <summary>
    /// 城市管理
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class CityList : BackPage
    {
        //所有默认城市
        public EyouSoft.Model.SysStructure.MSysCountry CountryModel = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Operation == "setcity")
            {
                SetCity();
            }
            if (!IsPostBack)
            {
                PageInit();
            }
        }

        protected void PageInit()
        {
            //获得默认城市
            CountryModel = new EyouSoft.BLL.SysStructure.BGeography().GetList();
            //所有常用城市
            IList<int> CityIds = new EyouSoft.BLL.ComStructure.BComCity().GetCityId(SiteUserInfo.CompanyId);
            foreach (int item in CityIds)
            {
                hidCommonCity.Value += item.ToString() + ",";
            }
        }

        private void SetCity()
        {
            bool flag = new EyouSoft.BLL.ComStructure.BComCity().SetCity(CityId, SiteUserInfo.CompanyId);
            if (flag)
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write("设置成功");
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write("设置失败");
                Response.End();
            }
        }

        public string Operation { get { return Utils.GetQueryStringValue("oper"); } }

        public int CityId { get { return Utils.GetInt(Utils.GetQueryStringValue("cityid")); } }


        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_城市管理栏目))
            {

            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_线路区域栏目))
            {
                Utils.TopRedirect("/SystemSet/RouterAreaList.aspx?memuid=2&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_模板管理栏目))
            {
                Utils.TopRedirect("/SystemSet/TourItemList.aspx?memuid=3&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_报价标准栏目))
            {
                Utils.TopRedirect("/SystemSet/QuoteStandardList.aspx?memuid=4&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_客户等级栏目))
            {
                Utils.TopRedirect("/SystemSet/GuestLevelList.aspx?memuid=5&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目))
            {
                Utils.TopRedirect("/SystemSet/PayStyleList.aspx?memuid=6&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_会员管理栏目))
            {
                Utils.TopRedirect("/SystemSet/MemberTypeList.aspx?memuid=7&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_保险类型栏目))
            {
                Utils.TopRedirect("/SystemSet/InsuranceList.aspx?memuid=8&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_上车地点管理栏目))
            {
                Utils.TopRedirect("/systemset/carplacelist.aspx?memuid=9&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_车型管理栏目))
            {
                Utils.TopRedirect("/systemset/cartypelist.aspx?memuid=10&sl=" + Utils.GetQueryStringValue("sl"));
            }
            else
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_栏目, false);
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.general;
        }
    }
}
