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
    /// YL-网站介绍相关
    /// </summary>
    public partial class WangZhan : EyouSoft.Common.Page.BackPage
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
            if (!this.CheckGrant(Privs.游轮管理_网站介绍_栏目))
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
            var info1 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.维诗达简介);
            var info2 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.联系我们);
            var info3 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站版权);
            var info4 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站标题);
            var info5 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站关键字);
            var info6 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站描述);
            var info7 = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.企业文化);
            txtJianJie.Value = info1.V;
            txtLianXiWoMen.Value = info2.V;
            txtBanQuan.Value = info3.V;
            txtTitle.Value = info4.V;
            txtKeywords.Value = info5.V;
            txtDescription.Value = info6.V;
            txtV7.Value = info7.V;
            var zizhi = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.旅游度假资质);
            txtzizhi.Value = zizhi.V;
            var guanggao = bll.GetKvInfo(CurrentUserCompanyID, EyouSoft.Model.EnumType.YlStructure.WzKvKey.广告业务);
            txtguanggao.Value = guanggao.V;
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
            var info6 = new EyouSoft.Model.YlStructure.MWzKvInfo();
            var info7 = new EyouSoft.Model.YlStructure.MWzKvInfo();

            info1.CompanyId = CurrentUserCompanyID;
            info1.IssueTime = DateTime.Now;
            info1.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.维诗达简介;
            info1.OperatorId = SiteUserInfo.UserId;
            info1.V = Utils.GetYlEditorText(Request.Form[txtJianJie.UniqueID]);

            info2.CompanyId = CurrentUserCompanyID;
            info2.IssueTime = DateTime.Now;
            info2.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.联系我们;
            info2.OperatorId = SiteUserInfo.UserId;
            info2.V = Utils.GetYlEditorText(Request.Form[txtLianXiWoMen.UniqueID]);

            info3.CompanyId = CurrentUserCompanyID;
            info3.IssueTime = DateTime.Now;
            info3.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站版权;
            info3.OperatorId = SiteUserInfo.UserId;
            info3.V = Utils.GetYlEditorText(Request.Form[txtBanQuan.UniqueID]);

            info4.CompanyId = CurrentUserCompanyID;
            info4.IssueTime = DateTime.Now;
            info4.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站标题;
            info4.OperatorId = SiteUserInfo.UserId;
            info4.V = Utils.GetFormValue(txtTitle.UniqueID);

            info5.CompanyId = CurrentUserCompanyID;
            info5.IssueTime = DateTime.Now;
            info5.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站关键字;
            info5.OperatorId = SiteUserInfo.UserId;
            info5.V = Utils.GetFormValue(txtKeywords.UniqueID);

            info6.CompanyId = CurrentUserCompanyID;
            info6.IssueTime = DateTime.Now;
            info6.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站描述;
            info6.OperatorId = SiteUserInfo.UserId;
            info6.V = Utils.GetFormValue(txtDescription.UniqueID);

            info7.CompanyId = CurrentUserCompanyID;
            info7.IssueTime = DateTime.Now;
            info7.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.企业文化;
            info7.OperatorId = SiteUserInfo.UserId;
            info7.V = Utils.GetYlEditorText(Request.Form[txtV7.UniqueID]);


            bll.SheZhiKvInfo(info1);
            bll.SheZhiKvInfo(info2);
            bll.SheZhiKvInfo(info3);
            bll.SheZhiKvInfo(info4);
            bll.SheZhiKvInfo(info5);
            bll.SheZhiKvInfo(info6);
            bll.SheZhiKvInfo(info7);

            var zizhi = new EyouSoft.Model.YlStructure.MWzKvInfo();
            zizhi.CompanyId = CurrentUserCompanyID;
            zizhi.IssueTime = DateTime.Now;
            zizhi.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.旅游度假资质;
            zizhi.OperatorId = SiteUserInfo.UserId;
            zizhi.V = Utils.GetYlEditorText(Request.Form[txtzizhi.UniqueID]);
            bll.SheZhiKvInfo(zizhi);
            var guanggao = new EyouSoft.Model.YlStructure.MWzKvInfo();
            guanggao.CompanyId = CurrentUserCompanyID;
            guanggao.IssueTime = DateTime.Now;
            guanggao.K = EyouSoft.Model.EnumType.YlStructure.WzKvKey.广告业务;
            guanggao.OperatorId = SiteUserInfo.UserId;
            guanggao.V = Utils.GetYlEditorText(Request.Form[txtguanggao.UniqueID]);
            bll.SheZhiKvInfo(guanggao);

            RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
        }
        #endregion
    }
}
