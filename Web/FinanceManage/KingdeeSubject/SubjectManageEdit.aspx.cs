using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.KingDee;

namespace EyouSoft.Web.FinanceManage.KingdeeSubject
{
    /// <summary>
    /// 金蝶科目-科目管理
    /// 添加-修改-
    /// </summary>
    public partial class SubjectManageEdit : BackPage
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
            MKingDeeSubject model = new BFinance().GetKingDeeSubjectMdl(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("SubjectId")));
            if (model != null)
            {
                txt_ChkItem.Text = model.ChkItem + "|" + model.ChkId;
                txt_MnemonicCd.Text = model.MnemonicCd;
                txt_SubjectCd.Text = model.SubjectCd;
                txt_SubjectNm.Text = model.SubjectNm + "|" + model.ItemId;
                txt_SubjectType.Text = model.PreSubjectNm + "|" + model.PreSubjectId;
                sel_Xy.SelectedValue = ((int)model.SubjectTyp).ToString();
                txt_SubjectCd.Enabled = false;
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
            MKingDeeSubject model = new MKingDeeSubject();
            string msg = string.Empty;
            if (GetVal(model, ref msg))
            {
                string[] msgarr = { 
                                      UtilsCommons.AjaxReturnJson("1"), 
                                      UtilsCommons.AjaxReturnJson("-1", "保存失败!"), 
                                      UtilsCommons.AjaxReturnJson("-1", "科目代码已存在"),
                                      UtilsCommons.AjaxReturnJson("-1", "该科目已被使用")};
                AjaxResponse(msgarr[new BFinance().AddOrUpdKingDeeSubject(model)]);
            }
            AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
        }
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool GetVal(MKingDeeSubject model, ref string msg)
        {
            model = model ?? new MKingDeeSubject();
            //科目编号(添加没值,修改有值)
            model.SubjectId = Utils.GetInt(Utils.GetQueryStringValue("SubjectId"));
            //系统公司编号
            model.CompanyId = CurrentUserCompanyID;
            //核算项目名称
            model.ChkItem = Utils.GetFormValue("ChkItem");
            //核算项目ID
            model.ChkId = Utils.GetFormValue("ChkItemId");
            //助记码
            model.MnemonicCd = Utils.GetFormValue("MnemonicCd");
            //父级科目编号
            model.PreSubjectId = Utils.GetInt(Utils.GetFormValue("PreSubjectId"));
            //父级科目名称
            model.PreSubjectNm = Utils.GetFormValue("PreSubjectNm");
            //科目代码
            model.SubjectCd = Utils.GetFormValue("SubjectCd");
            //科目名称
            model.SubjectNm = Utils.GetFormValue("SubjectNm");
            //科目类型
            model.SubjectTyp = (FinanceAccountItem)Utils.GetInt(Utils.GetFormValue("SubjectTyp"));
            model.ItemId = Utils.GetFormValue("ItemId");
            msg += model.SubjectCd.Length > 0 ? string.Empty : "科目代码不能为空!<br/>";
            msg += model.SubjectNm.Length > 0 ? string.Empty : "科目名称不能为空!<br/>";
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
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_科目管理栏目))
                {
                    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶科目_科目管理栏目, false);
                }
            }
        }
    }
}
