using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.KingDee;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;

namespace EyouSoft.Web.FinanceManage.KingdeeSubject
{
    /// <summary>
    /// 金蝶核算项目--核算项目管理
    /// 添加--修改
    /// </summary>
    public partial class AdjustItemEdit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("doType").Length > 0)
            {
                Save();
            }
            DataInit();

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            sel_Xy.DataValueField = "Value";
            sel_Xy.DataTextField = "Text";
            sel_Xy.DataSource = EnumObj.GetList(typeof(FinanceAccountItem));
            sel_Xy.DataBind();

            MKingDeeChk model = new BFinance().GetKingDeeChkMdl(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("SubjectId")));
            if (model != null)
            {
                txt_ChkCd.Text = model.ChkCd + "|" + model.PreChkId;
                txt_MnemonicCd.Text = model.MnemonicCd;
                txt_ChkNm.Text = model.ChkNm + "|" + model.ItemId;
                txt_SubjectType.Text = model.PreChkNm + "|" + model.PreChkId;
                sel_Xy.SelectedValue = ((int)model.ChkCate).ToString();
                txt_ChkCd.Enabled = false;
            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            MKingDeeChk model = new MKingDeeChk();
            string msg = string.Empty;
            if (GetVal(model, ref msg))
            {
                string[] msgarr = { 
                                      UtilsCommons.AjaxReturnJson("1"), 
                                      UtilsCommons.AjaxReturnJson("-1", "保存失败!"),
                                      UtilsCommons.AjaxReturnJson("-1", "核算项目代码已存在"),
                                      UtilsCommons.AjaxReturnJson("-1", "父级核算项目已被使用"),
                                      UtilsCommons.AjaxReturnJson("-1", "该核算项目已被使用")};
                AjaxResponse(msgarr[new BFinance().AddOrUpdKingDeeChk(model)]);
            }
            AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
        }
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool GetVal(MKingDeeChk model, ref string msg)
        {
            model = model ?? new MKingDeeChk();
            //核算项目编号(添加没值,修改有值)
            model.ChkId = Utils.GetInt(Utils.GetQueryStringValue("SubjectId"));
            //系统公司编号
            model.CompanyId = CurrentUserCompanyID;
            //核算项目名称
            model.ChkNm = Utils.GetFormValue("ChkNm");
            //核算项目代码
            model.ChkCd = Utils.GetFormValue("ChkCd");
            //助记码
            model.MnemonicCd = Utils.GetFormValue("MnemonicCd");
            //父级核算项目编号
            model.PreChkId = Utils.GetInt(Utils.GetFormValue("PreChkId"));
            //父级核算项目名称
            model.PreChkNm = Utils.GetFormValue("PreChkNm");
            //核算项目类型
            model.ChkCate = (FinanceAccountItem)Utils.GetInt(Utils.GetFormValue("ChkCate"));
            model.ItemId = Utils.GetFormValue("ItemId");
            msg += model.ChkCd.Length > 0 ? string.Empty : "核算项目代码不能为空!<br/>";
            msg += model.ChkNm.Length > 0 ? string.Empty : "核算项目名称不能为空!<br/>";
            return msg.Length <= 0;
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
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_核算项目管理栏目))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_核算项目管理栏目, false);
                }
            }
        }
    }
}
