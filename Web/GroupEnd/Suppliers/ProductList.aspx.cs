using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Web.UI.HtmlControls;

namespace Web.GroupEnd.Suppliers
{

    public partial class ProductList : SupplierPage
    {

        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        public int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;





        protected void Page_Load(object sender, EventArgs e)
        {
            //Ajax
            string type = Request.Params["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Delete"))
                {
                    Response.Write(DoDelete());
                    Response.End();
                }
            }

            if (!IsPostBack)
            {
                //公告
                this.Suppliers1.CompanyId = SiteUserInfo.CompanyId;

                //绑定数据、分页控件
                BindSource();



            }

        }






        /// <summary>
        /// 绑定数据源、分页控件
        /// </summary>
        private void BindSource()
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);

            EyouSoft.Model.TourStructure.MTourSupplierSearch search = new EyouSoft.Model.TourStructure.MTourSupplierSearch();
            search.TourCode = Utils.GetQueryStringValue("txtTourCode");
            search.RouteName = Utils.GetQueryStringValue("txtRouteName");
            search.AreaId = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"), 0);
            search.SLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBeginLDate"));
            search.LLDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndLDate"));         
            search.RealPeopleNumber = Utils.GetIntNull(Utils.GetQueryStringValue(this.CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));
            search.RealPeopleNumberManipulate = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(this.CaiWuShaiXuan1.ClientUniqueIDOperator));

            EyouSoft.BLL.TourStructure.BTour bTour = new EyouSoft.BLL.TourStructure.BTour();

            IList<EyouSoft.Model.TourStructure.MTourSanPinInfo> list = bTour.GetTourSupplierList(SiteUserInfo.SourceCompanyInfo.CompanyId, pageSize, pageIndex, ref  recordCount, search);
            this.RpTour.DataSource = list;
            this.RpTour.DataBind();


            BindPage();

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            if (recordCount == 0)
            {
                this.PhPage.Visible = false;
                this.litMsg.Visible = true;
            }
            else
            {
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        private string DoDelete()
        {
            string msg = UtilsCommons.AjaxReturnJson("0", "计划删除 失败！");
            string id = Utils.GetFormValue("id");
            if (!string.IsNullOrEmpty(id))
            {
                string[] ids = id.Split('|');
                EyouSoft.BLL.TourStructure.BTour bTour = new EyouSoft.BLL.TourStructure.BTour();
                if (bTour.DeleteTour(SiteUserInfo.CompanyId, ids))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "计划删除 成功！");
                }
            }
            return msg;
        }


        /// <summary>
        /// 订单信息汇总表
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintPage(string tourId, string type)
        {
            string url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.订单信息汇总表);
            url = url + "?tourId=" + tourId + "&type=" + type;
            return url;

        }


        /// <summary>
        /// 游客名单
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintPage(string tourId)
        {
            string url = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.游客名单);
            url = url + "?tourId=" + tourId;
            return url;

        }

    }
}
