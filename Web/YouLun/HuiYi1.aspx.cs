using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.EnumType.PrivsStructure;
using EyouSoft.Common;

namespace EyouSoft.Web.YouLun
{
    /// <summary>
    /// YL-长江游轮会议
    /// </summary>
    public partial class HuiYi1 : EyouSoft.Common.Page.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(Privs.游轮管理_会议管理_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var bll = new EyouSoft.BLL.YlStructure.BWz();
            var info1 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议接待);
            var info2 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议场地选择);
            var info3 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议服务项目及流程);
            txtV1.Value = info1.V;
            txtV2.Value = info2.V;
            txtV3.Value = info3.V;
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            if (Request.HttpMethod.ToLower() != "post") RCWE(UtilsCommons.AjaxReturnJson("0", "异常请求"));

            var bll = new EyouSoft.BLL.YlStructure.BWz();
            var info1 = new EyouSoft.Model.YlStructure.MWzKvInfo();
            var info2 = new EyouSoft.Model.YlStructure.MWzKvInfo();
            var info3 = new EyouSoft.Model.YlStructure.MWzKvInfo();

            info1.CompanyId = CurrentUserCompanyID;
            info1.IssueTime = DateTime.Now;
            info1.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议接待;
            info1.OperatorId = SiteUserInfo.UserId;
            info1.V = Utils.GetYlEditorText(Request.Form[txtV1.UniqueID]);

            info2.CompanyId = CurrentUserCompanyID;
            info2.IssueTime = DateTime.Now;
            info2.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议场地选择;
            info2.OperatorId = SiteUserInfo.UserId;
            info2.V = Utils.GetYlEditorText(Request.Form[txtV2.UniqueID]);

            info3.CompanyId = CurrentUserCompanyID;
            info3.IssueTime = DateTime.Now;
            info3.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮小型会议服务项目及流程;
            info3.OperatorId = SiteUserInfo.UserId;
            info3.V = Utils.GetYlEditorText(Request.Form[txtV3.UniqueID]);

            bll.SheZhiKvInfo(info1);
            bll.SheZhiKvInfo(info2);
            bll.SheZhiKvInfo(info3);

            RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
        }
        #endregion
    }
}
