using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;
using EyouSoft.Model.YlStructure;
using Web.YouLun.WUC;

namespace EyouSoft.Web.YouLun
{
    public partial class ZiYuanAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
                initPage();
            }
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        void BaoCun()
        {
            string msg = string.Empty;
            string Id = Utils.GetQueryStringValue("id");
            EyouSoft.Model.YlStructure.MGongSiInfo model = new EyouSoft.Model.YlStructure.MGongSiInfo();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.UserId;


            model.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            model.JianYaoJieShao = Utils.GetFormValue(txtJieShao.UniqueID);
            model.XiangXiJieShao = Utils.GetYlEditorText(Request.Form[txtXXJieShao.UniqueID]);
            model.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetFormValue(ddlGSType.UniqueID));

            #region logo
            var items1 = upload1.Files;
            var items2 = upload1.YuanFiles;

            if (items1 != null && items1.Count > 0)
            {
                model.Logo = items1[0].FilePath;
            }
            else if (items2 != null && items2.Count > 0)
            {
                model.Logo = items2[0].FilePath;
            }
            #endregion


            #region fujians
            var items4 = upload2.Files;
            var items5 = upload2.YuanFiles;
            var items6 = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            if (items4 != null && items4.Count > 0)
            {
                foreach (var item in items4)
                {
                    var item1 = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item1.Filepath = item.FilePath;
                    items6.Add(item1);
                }
            }

            if (items5 != null && items5.Count > 0)
            {
                foreach (var item in items5)
                {
                    var item1 = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item1.Filepath = item.FilePath;
                    items6.Add(item1);
                }
            }

            model.FuJians = items6;
            #endregion

            model.PaiXuId = Utils.GetInt(Utils.GetFormValue(txtPaiXuId.UniqueID));
            model.YouShi=Utils.GetYlEditorText(Request.Form[txtYouShi.UniqueID]);
            model.RongYu = Utils.GetYlEditorText(Request.Form[txtRongYu.UniqueID]);

            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().InsertGongSi(model) == 1)
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
                model.GongSiId = Id;
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().UpdateGongSi(model) == 1)
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


            this.ddlGSType.DataTextField = "Text";
            this.ddlGSType.DataValueField = "Value";
            this.ddlGSType.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing));
            this.ddlGSType.DataBind();

            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSiInfo(Utils.GetQueryStringValue("id"));
            if (model != null)
            {
                txtMingCheng.Text = model.MingCheng;
                if (ddlGSType.Items.FindByValue(((int)model.LeiXing).ToString()) != null)
                {
                    ddlGSType.Items.FindByValue(((int)model.LeiXing).ToString()).Selected = true;
                }                
                txtJieShao.Text = model.JianYaoJieShao;
                txtXXJieShao.Text = model.XiangXiJieShao;

                #region logo
                MFileInfo file = new MFileInfo();
                file.FilePath = model.Logo;
                var items = new List<MFileInfo>();
                items.Add(file);
                upload1.YuanFiles = items;
                #endregion

                #region fujians
                var items1 = new List<MFileInfo>();
                if (model.FuJians != null && model.FuJians.Count > 0)
                {
                    foreach (var item in model.FuJians)
                    {
                        MFileInfo file1 = new MFileInfo();
                        file1.FilePath = item.Filepath;
                        items1.Add(file1);
                    }
                }
                upload2.YuanFiles = items1;
                #endregion

                txtPaiXuId.Text = model.PaiXuId.ToString();

                txtYouShi.Text = model.YouShi;
                txtRongYu.Text = model.RongYu;
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
    }
}
