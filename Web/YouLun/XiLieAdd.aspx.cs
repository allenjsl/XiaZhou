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
    public partial class XiLieAdd : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            YLCompanySelect1.T1 = 255;

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
            EyouSoft.Model.YlStructure.MXiLieInfo model = new EyouSoft.Model.YlStructure.MXiLieInfo();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.UserId;

            model.GongSiId = Utils.GetFormValue(YLCompanySelect1.CompIDClient);
            model.JianYaoJieShao = Utils.GetFormValue(txtJieShao.UniqueID);
            model.XiangXiJieShao = Utils.GetYlEditorText(Request.Form[txtXXJieShao.UniqueID]);
            model.MingCheng = Utils.GetFormValue(txtXiLie.UniqueID);

            #region fujians
            var items1 = upload1.Files;
            var items2 = upload1.YuanFiles;
            var items3 = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            if (items1 != null && items1.Count > 0)
            {
                foreach (var item in items1)
                {
                    var item1 = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item1.Filepath = item.FilePath;
                    items3.Add(item1);
                }
            }

            if (items2 != null && items2.Count > 0)
            {
                foreach (var item in items2)
                {
                    var item1 = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item1.Filepath = item.FilePath;
                    items3.Add(item1);
                }
            }

            model.FuJians = items3;
            #endregion

            model.PaiXuId = Utils.GetInt(Utils.GetFormValue(txtPaiXuId.UniqueID));

            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().InsertXiLie(model) == 1)
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
                model.XiLieId = Id;
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().UpdateXiLie(model) == 1)
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
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLieInfo(Utils.GetQueryStringValue("id"));
            if (model != null)
            {
                YLCompanySelect1.CompName = model.GongSiMingCheng;//游轮公司
                YLCompanySelect1.ComPid = model.GongSiId;//游轮公司ID
                txtXiLie.Text = model.MingCheng;                
                txtJieShao.Text = model.JianYaoJieShao;//介绍
                txtXXJieShao.Text = model.XiangXiJieShao;//详细介绍

                var items = new List<MFileInfo>();

                if (model.FuJians != null && model.FuJians.Count > 0)
                {
                    foreach (var item in model.FuJians)
                    {
                        MFileInfo file = new MFileInfo();
                        file.FilePath = item.Filepath;
                        items.Add(file);
                    }
                }
                upload1.YuanFiles = items;

                txtPaiXuId.Text = model.PaiXuId.ToString();
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
