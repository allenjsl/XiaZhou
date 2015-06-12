using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.YouLun
{
    public partial class JiChuXinXiAdd : BackPage
    {
        protected Model.EnumType.YlStructure.YouLunLeiXing YouLunLeiXing = Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
        protected int PXinXiId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("doajax") == "GetGangKouBieMingDDL")
            {
                Response.Clear();
                Response.Write(this.GetGangKouBieMingDDL((EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("youlunleixing"))));
                Response.End();
            }
            if (!IsPostBack)
            {
                initPage();
            }
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
            if (Utils.GetQueryStringValue("type") == "2") PlaceHolder1.Visible = false;

        }
        /// <summary>
        /// 保存数据
        /// </summary>
        void BaoCun()
        {
            string msg = string.Empty;
            int Id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            EyouSoft.Model.YlStructure.MJiChuXinXiInfo model = new EyouSoft.Model.YlStructure.MJiChuXinXiInfo();
            model.MingCheng = Utils.GetFormValue(txtText.UniqueID);
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorId = SiteUserInfo.UserId;
            model.IssueTime = DateTime.Now;
            model.LeiXing = (EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing)Utils.GetInt(Utils.GetQueryStringValue("type"));
            if (model.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.国籍)
            {
                model.ChangJingLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
            }
            else
            {
                model.ChangJingLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetFormValue(ddlType.UniqueID));

            }
            model.PaiXuId = Utils.GetInt(Utils.GetFormValue(txtPaiXuId.UniqueID));
            model.BieMing = Utils.GetFormValue(txtBieMing.UniqueID);
            model.PXinXiId = Utils.GetInt(Utils.GetFormValue("ddlGangKouBieMing"));

            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (Id == 0)
            {
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().InsertJiChuXinXi(model) == 1)
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                }
            }
            else
            {
                model.XinXiId = Id;
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().UpdateJiChuXinXi(model) == 1)
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                }
            }
            Response.End();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        void initPage()
        {

            this.ddlType.DataTextField = "Text";
            this.ddlType.DataValueField = "Value";
            this.ddlType.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing));
            this.ddlType.DataBind();

            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXiInfo(Utils.GetInt(Utils.GetQueryStringValue("id")));
            if (model != null)
            {
                if (model.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线 || model.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口)
                    PlaceHolder2.Visible = true;
                phdBieMing.Visible = model.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口;
                phdGangKouBieMing.Visible = model.LeiXing == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线;
                txtText.Text = model.MingCheng;
                if (ddlType.Items.FindByValue(((int)model.ChangJingLeiXing).ToString()) != null)
                    ddlType.Items.FindByValue(((int)model.ChangJingLeiXing).ToString()).Selected = true;

                txtPaiXuId.Value = model.PaiXuId.ToString();
                txtBieMing.Text = model.BieMing;
                PXinXiId = model.PXinXiId;
                YouLunLeiXing = model.ChangJingLeiXing;
            }
            else
            {
                EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing lx = (EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing)Utils.GetInt(Utils.GetQueryStringValue("type"));
                if (lx == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线 || lx == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口)
                    PlaceHolder2.Visible = true;
                phdBieMing.Visible = lx == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口;
                phdGangKouBieMing.Visible = lx == EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线;
            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        void PowerControl()
        {
            //if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目))
            //{
            //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目, false);
            //    return;
            //}
        }

        /// <summary>
        /// 根据游轮类型获取港口别名选择列表
        /// </summary>
        /// <param name="youlunleixing"></param>
        /// <returns></returns>
        protected string GetGangKouBieMingDDL(Model.EnumType.YlStructure.YouLunLeiXing youlunleixing)
        {
            var s = new System.Text.StringBuilder("<option value=\"0\">请选择</option>");
            var l = new BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo() { LeiXing = Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口, YouLunLeiXing=youlunleixing });
            if (l != null && l.Count > 0)
            {
                foreach (var m in l)
                {
                    if (!string.IsNullOrEmpty(m.BieMing))
                    {
                        s.AppendFormat("<option value=\"{0}\" {2}>{1}</option>",m.XinXiId,m.BieMing,m.XinXiId==PXinXiId?"selected=\"selected\"":"");
                    }
                }
            }
            return s.ToString();
        }
    }
}
