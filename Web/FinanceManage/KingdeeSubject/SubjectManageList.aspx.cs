using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using System.Collections;
using EyouSoft.Common;

namespace EyouSoft.Web.FinanceManage.KingdeeSubject
{
    /// <summary>
    /// 金蝶科目-科目管理-
    /// </summary>
    public partial class SubjectManageList : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            #region 删除
            if (Utils.GetQueryStringValue("doType") == "delete")
            {
                DeleteData();
            }
            #endregion
            DataInit();
        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DataInit()
        {
            #region 分页参数
            int pageSize = 20;
            int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            int recordCount = 0;
            #endregion
            IList<MKingDeeSubject> ls = new BFinance().GetKingDeeSubjectLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId);
            if (ls != null && ls.Count > 0)
            {
                pan_msg.Visible = false;
                prt_list.DataSource = ls;
                prt_list.DataBind();
                BindPage(pageSize, pageIndex, recordCount);
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        #endregion
        /// <summary>
        /// 删除
        /// </summary>
        private void DeleteData()
        {
            int[] ids = Utils.ConvertToIntArray(Utils.GetQueryStringValue("ids"));
            string userd = string.Empty;
            int flag = new BFinance().DelKingDeeSubject(this.SiteUserInfo.CompanyId, ref userd, ids);
            string[] msgarr = { 
                                  UtilsCommons.AjaxReturnJson("1", "删除成功！"), 
                                  UtilsCommons.AjaxReturnJson("-1", "删除失败!"), 
                                  UtilsCommons.AjaxReturnJson("-1", userd + "删除执行终止!") };
            AjaxResponse(msgarr[flag]);
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
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_栏目, false);
            }
            else
            {
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_科目管理栏目))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_科目管理栏目, false);
                }
            }
        }

    }
}
