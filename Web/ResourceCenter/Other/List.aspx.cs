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

namespace EyouSoft.Web.ResourceCenter.Other
{
    using EyouSoft.BLL.ComStructure;
    using EyouSoft.BLL.SourceStructure;
    using EyouSoft.Common;
    using EyouSoft.Common.Page;
    using EyouSoft.Model.EnumType.PrivsStructure;
    using EyouSoft.Model.SourceStructure;

    public partial class List : BackPage
    {
        protected string YKQRDPrintUri = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            //初始化
            DataInit();
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            BComSetting comSettingBll = new BComSetting();
            YKQRDPrintUri = comSettingBll.GetPrintUri(CurrentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.其他预控确认单);
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            #region 查询实体
            var queryModel = new MSourceSueOtherSearch();
            queryModel.CompanyId = CurrentUserCompanyID;
            //支出类别
            queryModel.TypeName = Utils.GetQueryStringValue("rType");
            //其他公司名称
            queryModel.SourceName = Utils.GetQueryStringValue("tavName");
            //有效时间--始
            queryModel.StartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("dateTimeS"));
            //有效时间--终
            queryModel.EndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("dateTimeE"));
            #endregion
            var ls = new BSourceControl().GetListSueOther(pageIndex, pageSize, ref recordCount, queryModel);
            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage(int pageSize, int pageIndex, int recordCount)
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.资源预控_其他预控_栏目))
            {
                Utils.ResponseNoPermit(Privs.资源预控_其他预控_栏目, true);
                return;
            }
            pan_Add.Visible = CheckGrant(Privs.资源预控_其他预控_新增);
        }

        #endregion
    }
}
