using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.BLL;
using System.Data;

namespace Web.CustomerCenter.Unit
{
    /// <summary>
    /// 客户管理 单位直客 列表
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public partial class CustomerList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            switch (doType)
            {
                case "delete":
                    Response.Clear();
                    Response.Write(DeleteData(Utils.GetQueryStringValue("id")));
                    Response.End();
                    break;
            }
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            int sl = Utils.GetInt(Utils.GetQueryStringValue("sl"), 0);
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"),1);
            //省份
            int dplProvince = Utils.GetInt(Utils.GetQueryStringValue("ddlProvice"));
            //城市
            int dplCity = Utils.GetInt(Utils.GetQueryStringValue("ddlCity"));
            //单位名称
            string txtUnitName = Utils.GetQueryStringValue("txtUnitName");
            //联系人
            //string txtlinkManName = Utils.GetQueryStringValue("txtlinkManName");
            //客户等级
            int ddlLevId = Utils.GetInt(Utils.GetQueryStringValue("ddlLevId"));
            //责任销售
            string txtSellerId = Utils.GetQueryStringValue("txtSellerId");

            EyouSoft.BLL.CrmStructure.BCrm crmBll = new EyouSoft.BLL.CrmStructure.BCrm();
            //EyouSoft.Model.CrmStructure.MCustomerListModel searchModel = new EyouSoft.Model.CrmStructure.MCustomerListModel();
            //searchModel.CityId = dplCity;
            //searchModel.CompanyId = base.SiteUserInfo.CompanyId;
            //searchModel.LevId = ddlLevId;
            //searchModel.Name = txtUnitName;
            //searchModel.ProvinceId = dplProvince;
            //searchModel.SellerId = txtSellerId;
            //searchModel.DeptId = base.SiteUserInfo.DeptId;
            //searchModel.LinkManName = txtlinkManName;
            //searchModel.Type = EyouSoft.Model.EnumType.CrmStructure.CrmType.单位直客;
            //IList<EyouSoft.Model.CrmStructure.MCustomerListModel> list = crmBll.GetUnitCustomerShowModel(searchModel, pageIndex, pageSize, ref recordCount);
            object list = null;
            if (list != null)
            {
                rptList.DataSource = list;
                rptList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                rptList.Controls.Add(new Label() { Text = "对不起,没有相关数据!" });
                ExporPageInfoSelect1.Visible = false;
                ExporPageInfoSelect2.Visible = false;
            }

        }

        /// <summary>
        /// 绑定客户等级
        /// </summary>
        /// <returns></returns>
        protected string BindCrmLevId()
        {
            EyouSoft.BLL.ComStructure.BComLev levBll = new EyouSoft.BLL.ComStructure.BComLev();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComLev> list = levBll.GetList(base.SiteUserInfo.CompanyId);
            sb.Append("<option value=\"0\">--未选择--</option>");
            for (int i = 0; i < list.Count; i++)
            {
                if (Utils.GetQueryStringValue("ddlLevId") == list[i].Id.ToString())
                sb.AppendFormat("<option value=\"{0}\" selected=\"true\">{1}</option>", list[i].Id, list[i].Name);
            }
            return sb.ToString();
        }


        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string id)
        {
            string msg = string.Empty;
            EyouSoft.BLL.CrmStructure.BCrm crmBll = new EyouSoft.BLL.CrmStructure.BCrm();
            string[] crmIdList = id.Split(new char[] { ',' });
            int result = 0;//crmBll.DeleteUnitCustomerModel(crmIdList);
            if (result > 0)
            {
                msg = "1";
            }
            else
            {
                msg = "0";
            }
            return msg;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_栏目, false);
                return;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 某某方法
        /// </summary>
        /// <param name="i">参数1</param>
        /// <param name="s">参数2</param>
        protected void XXX(int i, string s)
        {

        }
        #endregion
    }
}
