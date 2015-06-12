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
    public partial class HuiYi2 : EyouSoft.Common.Page.BackPage
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
            var info1 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租流程);
            var info2 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议航线介绍);
            var info3 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租价格);
            var info4 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议会展服务);
            var info5 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议关切问题);
            txtV1.Value = info1.V;
            txtV2.Value = info2.V;
            txtV3.Value = info3.V;
            txtV4.Value = info4.V;
            txtV5.Value = info5.V;
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
            var info4 = new EyouSoft.Model.YlStructure.MWzKvInfo();
            var info5 = new EyouSoft.Model.YlStructure.MWzKvInfo();

            info1.CompanyId = CurrentUserCompanyID;
            info1.IssueTime = DateTime.Now;
            info1.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租流程;
            info1.OperatorId = SiteUserInfo.UserId;
            info1.V = Utils.GetYlEditorText(Request.Form[txtV1.UniqueID]);

            info2.CompanyId = CurrentUserCompanyID;
            info2.IssueTime = DateTime.Now;
            info2.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议航线介绍;
            info2.OperatorId = SiteUserInfo.UserId;
            info2.V = Utils.GetYlEditorText(Request.Form[txtV2.UniqueID]);

            info3.CompanyId = CurrentUserCompanyID;
            info3.IssueTime = DateTime.Now;
            info3.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议包租价格;
            info3.OperatorId = SiteUserInfo.UserId;
            info3.V = Utils.GetYlEditorText(Request.Form[txtV3.UniqueID]);

            info4.CompanyId = CurrentUserCompanyID;
            info4.IssueTime = DateTime.Now;
            info4.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议会展服务;
            info4.OperatorId = SiteUserInfo.UserId;
            info4.V = Utils.GetYlEditorText(Request.Form[txtV4.UniqueID]);

            info5.CompanyId = CurrentUserCompanyID;
            info5.IssueTime = DateTime.Now;
            info5.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮大型会议关切问题;
            info5.OperatorId = SiteUserInfo.UserId;
            info5.V = Utils.GetYlEditorText(Request.Form[txtV5.UniqueID]);

            bll.SheZhiKvInfo(info1);
            bll.SheZhiKvInfo(info2);
            bll.SheZhiKvInfo(info3);
            bll.SheZhiKvInfo(info4);
            bll.SheZhiKvInfo(info5);

            RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
        }
        #endregion
    }
}
