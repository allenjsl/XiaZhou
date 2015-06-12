using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.BLL;
using System.Data;
using EyouSoft.Model.EnumType.PrivsStructure;

namespace Web.CrmCenter
{
    /// <summary>
    /// 客户资料列表
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public partial class List : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex =1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        /// <summary>
        /// 二级栏目
        /// </summary>
        protected Menu2 Menu2Type = Menu2.None;
        /// <summary>
        /// 列表类型名称
        /// </summary>
        protected string ListTypeName = string.Empty;
   
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitMenu2Type();
            PowerControl();

            #region ajax request
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "delete":
                    Delete();
                    break;
                default: break;
            }
            #endregion
            
            DataInit();
        }

        #region private members
        /// <summary>
        /// 初始化二级栏目
        /// </summary>
        void InitMenu2Type()
        {
            Menu2Type = Utils.GetEnumValue<Menu2>(Utils.GetQueryStringValue("sl"), Menu2.None);

            switch (Menu2Type)
            {
                case Menu2.客户管理_单位直客: ListTypeName = "单位直客"; break;
                case Menu2.客户管理_同行客户: ListTypeName = "同行客户"; break;
                default: AjaxResponse("错误的请求"); break;
            }
        }

        /// <summary>
        /// get search info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.CrmStructure.MLBCrmSearchInfo GetSearchInfo()
        {
            var info = new EyouSoft.Model.CrmStructure.MLBCrmSearchInfo();
            
            info.CrmName = Utils.GetQueryStringValue("txtUnitName");
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlProvice"));
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCity"));
            info.SellerId = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient);
            info.SellerName = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);
            info.DengJiBH = Utils.GetIntNull(Utils.GetQueryStringValue("ddlLevId"));
            info.LxrName = Utils.GetQueryStringValue("txtLxrName");

            return info;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            EyouSoft.Model.EnumType.CrmStructure.CrmType crmType = EyouSoft.Model.EnumType.CrmStructure.CrmType.同行客户;
            if (Menu2Type == Menu2.客户管理_单位直客) crmType = EyouSoft.Model.EnumType.CrmStructure.CrmType.单位直客;

            var items = new EyouSoft.BLL.CrmStructure.BCrm().GetCrms(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, crmType, GetSearchInfo());
            if (items != null && items.Count>0)
            {
                rptList.DataSource = items;
                rptList.DataBind();
            }

            BindPage();
        }
       
        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            paging.UrlParams = Request.QueryString;
            paging.intPageSize = pageSize;
            paging.CurrencyPage = pageIndex;
            paging.intRecordCount = recordCount;

            if (paging.intRecordCount == 0)
            {
                paging.Visible = false;
                phEmpty.Visible = true;
            }
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        void Delete()
        {
            if (Menu2Type == Menu2.客户管理_同行客户
                && !this.CheckGrant(Privs.客户管理_同行客户_删除))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0"));
            }

            if (Menu2Type == Menu2.客户管理_单位直客
                && !CheckGrant(Privs.客户管理_单位直客_删除))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("0"));
            }

            string retCode = "0";
            string[] ids = Utils.GetQueryStringValue("deleteids").Split(',');

            if (ids != null && ids.Length > 0)
            {
                foreach (var s in ids)
                {
                    new EyouSoft.BLL.CrmStructure.BCrm().Delete(s, SiteUserInfo.UserId);
                }
            }

            retCode = "1";

            AjaxResponse(UtilsCommons.AjaxReturnJson(retCode));
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        void PowerControl()
        {
            if (Menu2Type == Menu2.客户管理_同行客户)
            {
                if (!this.CheckGrant(Privs.客户管理_同行客户_栏目))
                {
                    Utils.ResponseNoPermit(Privs.客户管理_同行客户_栏目, false);
                    return;
                }

                //操作栏
                if (CheckGrant(Privs.客户管理_同行客户_新增)) phXinZeng.Visible = true;
                if (CheckGrant(Privs.客户管理_同行客户_修改)) phXiuGai.Visible = true;
                if (CheckGrant(Privs.客户管理_同行客户_删除)) phShanChu.Visible = true;
            }

            if (Menu2Type == Menu2.客户管理_单位直客)
            {
                if (!this.CheckGrant(Privs.客户管理_单位直客_栏目))
                {
                    Utils.ResponseNoPermit(Privs.客户管理_单位直客_栏目, false);
                    return;
                }

                //操作栏
                if (CheckGrant(Privs.客户管理_单位直客_新增)) phXinZeng.Visible = true;
                if (CheckGrant(Privs.客户管理_单位直客_修改)) phXiuGai.Visible = true;
                if (CheckGrant(Privs.客户管理_单位直客_删除)) phShanChu.Visible = true;
            }
        }
        #endregion

        #region protected members
        /// <summary>
        /// 绑定客户等级
        /// </summary>
        /// <returns></returns>
        protected string BindCrmLevId()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            var items = new EyouSoft.BLL.ComStructure.BComLev().GetList(CurrentUserCompanyID);
            s.Append("<option value=\"\">--未选择--</option>");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.LevType == EyouSoft.Model.EnumType.ComStructure.LevType.内部结算价) continue;
                    s.AppendFormat("<option value=\"{0}\">{1}</option>", item.Id, item.Name);
                }
            }

            return s.ToString();
        }

        /// <summary>
        /// rptList_ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1) return;
            Literal ltrSuoZaiDi = (Literal)e.Item.FindControl("ltrSuoZaiDi");
            if (ltrSuoZaiDi == null) return;
            var info = (EyouSoft.Model.CrmStructure.MLBCrmInfo)e.Item.DataItem;
            if (info == null) return;
            if (info.CPCD == null) return;

            ltrSuoZaiDi.Text = info.CPCD.ProvinceName + "&nbsp;-&nbsp;" + info.CPCD.CityName;
        }
        #endregion
    }
}
