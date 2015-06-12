using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.CrmStructure;
using EyouSoft.BLL.CrmStructure;
using EyouSoft.Model.EnumType.CrmStructure;
using EyouSoft.Common;
using System.Xml.Linq;
using System.Text;

namespace Web.CommonPage
{
    /// <summary>
    /// 客户单位选用Ajax页面
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-3-29
    public partial class AjaxCustomerUnitSelect : BackPage
    {
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;
        /// <summary>
        /// 分页大小
        /// </summary>
        protected int pageSize = 36;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int pageIndex = 1;
        /// <summary>
        /// 客户单位类型
        /// </summary>
        protected CrmType type;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 分页参数

            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1); ;
            #endregion
            type = (CrmType)Utils.GetInt(Utils.GetQueryStringValue("currType"));

            MLBCrmSearchInfo queryString = new MLBCrmSearchInfo();
            queryString.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("cId"));
            queryString.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("pId"));
            queryString.CrmName = Utils.GetQueryStringValue("currName");
            queryString.SellerName = Utils.GetQueryStringValue("txtSellers");
            if (!string.IsNullOrEmpty(queryString.SellerName) && queryString.SellerName == SiteUserInfo.Name) queryString.SellerId = SiteUserInfo.UserId;

            IList<EyouSoft.Model.CrmStructure.MLBCrmXuanYongInfo> ls = new EyouSoft.BLL.CrmStructure.BCrm().GetCrmsXuanYong(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, type, queryString);
            if (ls != null && ls.Count > 0)
            {
                this.lbemptymsg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex);
            }
            else
            {
                this.lbemptymsg.Visible = true;
                this.phdPages.Visible = false;
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        protected string GetAttrStr(object ls)
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = (IList<EyouSoft.Model.CrmStructure.MCrmLinkman>)ls;
            if (list != null && list.Count > 0)
            {
                string mobilephone = " ";
                string contactname = " ";
                string contactphone = " ";
                string contactid = " ";
                string department = " ";
                foreach (MCrmLinkman item in list)
                {
                    contactid += item.Id + "|";
                    contactname += item.Name + "|";
                    contactphone += item.Telephone + "|";
                    mobilephone += item.MobilePhone + "|";
                    department += item.Department + "|";
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("data-mobilephone='{0}' data-contactname='{1}' data-contactphone='{2}' data-contactid='{3}' data-department='{4}'",
                    mobilephone.Substring(0, mobilephone.Length - 1).Trim(),
                    contactname.Substring(0, contactname.Length - 1).Trim(),
                    contactphone.Substring(0, contactphone.Length - 1).Trim(),
                    contactid.Substring(0, contactid.Length - 1).Trim(),
                    department.Substring(0, department.Length - 1).Trim()
                    );
                return sb.ToString();
            }
            return "data-mobilephone='' data-contactname='' data-contactphone='' data-contactid='' data-department=''";
        }

    }
}
