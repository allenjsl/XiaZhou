using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.PlanStructure;
using System.Collections.Generic;

namespace EyouSoft.Web.CommonPage
{
    public partial class Supplier : BackPage
    {
        /// <summary>
        /// 计调枚举
        /// </summary>
        protected EyouSoft.Model.EnumType.PlanStructure.PlanProject type;
        protected string AjaxURLg = string.Empty;
        protected string typename = string.Empty;
        /// <summary>
        /// 计调项枚举
        /// </summary>
        protected List<EnumObj> EnumSource = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //省份
            string Province = Utils.GetQueryStringValue("ddlProvice");
            //城市
            string City = Utils.GetQueryStringValue("ddlCity");
            //县区
            string Area = Utils.GetQueryStringValue("ddlArea");
            //名称
            string Name = Utils.GetQueryStringValue("txtName");
            //供应商类型
            type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Utils.GetInt(Utils.GetQueryStringValue("Sourcetype"));
            EnumSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject)).Where(m => m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游.ToString() && m.Text != EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料.ToString()).ToList();
            if (!IsPostBack)
            {
                PageInit(Province, City, Area, Name, type);
            }

        }
        private void PageInit(string provice, string city, string area, string name, PlanProject sourcetype)
        {
            //获取分页参数并强转
            AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?provice=" + provice + "&city=" + city + "&area=" + area + "&name=" + name + "&type=";
            switch (sourcetype)
            {
                case PlanProject.用车:
                    typename = "车队";
                    AjaxURLg += "car";
                    break;
                case PlanProject.用餐:
                    typename = "餐馆";
                    AjaxURLg += "hotel";
                    break;
                case PlanProject.地接:
                    typename = "地接社";
                    AjaxURLg += "ground";
                    break;
                case PlanProject.购物:
                    typename = "购物";
                    AjaxURLg += "shopping";
                    break;
                case PlanProject.景点:
                    typename = "景点";
                    AjaxURLg += "scenicspots";
                    break;
                case PlanProject.酒店:
                    typename = "酒店";
                    AjaxURLg += "wineshop";
                    break;
                case PlanProject.飞机:
                    typename = "票务";
                    AjaxURLg += "ticket";
                    break;
                case PlanProject.火车:
                    typename = "火车";
                    AjaxURLg += "ticket";
                    break;
                case PlanProject.汽车:
                    typename = "汽车";
                    AjaxURLg += "ticket";
                    break;
                case PlanProject.国内游轮:
                    typename = "游轮";
                    AjaxURLg += "cruise";
                    break;
                case PlanProject.其它:
                    typename = "其他";
                    AjaxURLg += "other";
                    break;
                case PlanProject.涉外游轮:
                    typename = "游轮";
                    AjaxURLg += "cruise";
                    break;

            }
        }
    }
}
