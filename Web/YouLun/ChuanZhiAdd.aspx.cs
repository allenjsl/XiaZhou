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
    public partial class ChuanZhiAdd : BackPage
    {
        protected string XiLieId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            YLCompanySelect1.T1 = 255;

            if (Utils.GetQueryStringValue("getXL") == "1") getXL();

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
            EyouSoft.Model.YlStructure.MChuanZhiInfo model = new EyouSoft.Model.YlStructure.MChuanZhiInfo();

            if (!string.IsNullOrEmpty(Id)) model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Id);

            model.CompanyId = SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.UserId;

            model.GongSiId = Utils.GetFormValue(YLCompanySelect1.CompIDClient);
            model.XiLieId = Utils.GetFormValue("txtXiLie");
            model.MingCheng = Utils.GetFormValue(txtchuanzhimingcheng.UniqueID);
            model.MingCheng1 = Utils.GetFormValue(txtyingwenmingcheng.UniqueID);
            model.XingJi = (EyouSoft.Model.EnumType.YlStructure.XingJi)Utils.GetInt(Utils.GetFormValue(ddlxingji.UniqueID));
            model.DunWei = Utils.GetFormValue(txtdunwei.UniqueID);
            model.XiaShuiRiQi = Utils.GetFormValue(txtxiashuiriqi.UniqueID);
            model.ZhuangXiuRiQi = Utils.GetFormValue(txtzhuangxiuriqi.UniqueID);
            model.ZaiKeLiang = Utils.GetFormValue(txtzaikeliang.UniqueID);
            model.JiaBanLouCeng = Utils.GetFormValue(txtjiaban.UniqueID);
            model.ChuanYuan = Utils.GetFormValue(txtchuanyuan.UniqueID);
            model.ChangDu = Utils.GetFormValue(txtchangdu.UniqueID);
            model.KuangDu = Utils.GetFormValue(txtkuandu.UniqueID);
            model.ChuanJi = Utils.GetFormValue(txtchuanji.UniqueID);
            model.ZuiGaoHangSu = Utils.GetFormValue(txtzuigaohangsu.UniqueID);
            model.ChuanZaiDianHua = Utils.GetFormValue(txtfuwutai.UniqueID);
            model.ChiShui = Utils.GetFormValue(txtChiShui.UniqueID);
            model.KeFangShuLiang = Utils.GetFormValue(txtKeFangShuLiang.UniqueID);

            #region pingmiantu
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

            model.PingMianTus = items3;
            #endregion

            #region chuanzhitupian
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


            model.JianYaoJieShao = Utils.GetFormValue(txtJieShao.UniqueID);
            model.XiangXiJieShao = Utils.GetYlEditorText(Request.Form[txtXXJieShao.UniqueID]);


            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().InsertChuanZhi(model) == 1)
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
                model.ChuanZhiId = Id;
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().UpdateChuanZhi(model) == 1)
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
            initDDL();
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Utils.GetQueryStringValue("id"));
            if (model != null)
            {

                YLCompanySelect1.CompName = model.GongSiMingCheng;
                YLCompanySelect1.ComPid = model.GongSiId;
                XiLieId = model.XiLieId;
                txtchuanzhimingcheng.Text = model.MingCheng;
                txtyingwenmingcheng.Text = model.MingCheng1;
                if (ddlxingji.Items.FindByValue(((int)model.XingJi).ToString()) != null)
                    ddlxingji.Items.FindByValue(((int)model.XingJi).ToString()).Selected = true;
                txtdunwei.Text = model.DunWei;
                txtxiashuiriqi.Text = model.XiaShuiRiQi;
                txtzhuangxiuriqi.Text = model.ZhuangXiuRiQi;
                txtzaikeliang.Text = model.ZaiKeLiang;
                txtjiaban.Text = model.JiaBanLouCeng;
                txtchuanyuan.Text = model.ChuanYuan;
                txtchangdu.Text = model.ChangDu;
                txtkuandu.Text = model.KuangDu;
                txtchuanji.Text = model.ChuanJi;
                txtzuigaohangsu.Text = model.ZuiGaoHangSu;
                txtfuwutai.Text = model.ChuanZaiDianHua;
                txtChiShui.Text = model.ChiShui;
                txtKeFangShuLiang.Text = model.KeFangShuLiang;

                #region pingmiantus
                var items = new List<MFileInfo>();

                if (model.PingMianTus != null && model.PingMianTus.Count > 0)
                {
                    foreach (var item in model.PingMianTus)
                    {
                        MFileInfo file = new MFileInfo();
                        file.FilePath = item.Filepath;
                        items.Add(file);
                    }
                }
                upload1.YuanFiles = items;
                #endregion

                #region fujians
                var items1 = new List<MFileInfo>();
                if (model.FuJians != null && model.FuJians.Count > 0)
                {
                    foreach (var item in model.FuJians)
                    {
                        MFileInfo file = new MFileInfo();
                        file.FilePath = item.Filepath;
                        items1.Add(file);
                    }
                }
                upload2.YuanFiles = items1;
                #endregion


                txtJieShao.Text = model.JianYaoJieShao;
                txtXXJieShao.Text = model.XiangXiJieShao;
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

        void initDDL()
        {
            this.ddlxingji.DataTextField = "Text";
            this.ddlxingji.DataValueField = "Value";
            this.ddlxingji.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.XingJi));
            this.ddlxingji.DataBind();
        }

        /// <summary>
        /// 获取系列下拉选项
        /// </summary>
        void getXL()
        {
            int recordCount = 0;
            string comPid = Utils.GetQueryStringValue("gongsi");
            string selectID = Utils.GetQueryStringValue("sid");
            StringBuilder xilieStr = new StringBuilder();
            xilieStr.Append("<option value=\"\">请选择</option>");
            var xilies = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(SiteUserInfo.CompanyId, 1000, 1, ref recordCount, new MXiLieChaXunInfo() { GongSiId = comPid });
            if (xilies != null && xilies.Count > 0)
            {
                for (int i = 0; i < xilies.Count; i++)
                {
                    if (xilies[i].XiLieId == selectID)
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\"  selected=\"selected\">{1}</option> ", xilies[i].XiLieId, xilies[i].MingCheng);
                    }
                    else
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\" >{1}</option>", xilies[i].XiLieId, xilies[i].MingCheng);
                    }
                }

            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("1", "", xilieStr.ToString()));
            Response.End();
        }
    }
}
