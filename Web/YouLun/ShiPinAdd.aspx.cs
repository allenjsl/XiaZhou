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
    public partial class ShiPinAdd : BackPage
    {

        protected string optXL = string.Empty, optCZ = string.Empty;

        protected string XiLieId = string.Empty;
        protected string ChuanZhiId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            YLCompanySelect1.T1 = 255;
            PowerControl();
            if (Utils.GetQueryStringValue("getXL") == "1") getXL();
            if (Utils.GetQueryStringValue("getCZ") == "1") getCZ();
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
            EyouSoft.Model.YlStructure.MShiPinInfo model = new EyouSoft.Model.YlStructure.MShiPinInfo();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.UserId;

            model.GongSiId = Utils.GetFormValue(YLCompanySelect1.CompIDClient);//游轮公司
            model.XiLieId = Utils.GetFormValue("txtXiLie");//系列编号
            model.ChuanZhiId = Utils.GetFormValue("txtChuanZhi");//船只编号
            model.MingCheng = Utils.GetFormValue(txtSPName.UniqueID);//视频名称
            model.Filepath = Utils.GetFormValue(txtURL.UniqueID);//视频地址
            model.XiangXiJieShao = Utils.GetFormValue(txtMiaoShu.UniqueID);//视频介绍

            var items = upload1.Files;
            var items1 = upload1.YuanFiles;

            if (items != null && items.Count > 0)
            {
                model.ShiPinIMG = items[0].FilePath;
            }
            else if (items1 != null && items1.Count > 0)
            {
                model.ShiPinIMG = items1[0].FilePath;
            }

            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().InsertShiPin(model) == 1)
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
                model.ShiPinId = Id;
                if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().UpdateShiPin(model) == 1)
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
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetShiPinInfo(Utils.GetQueryStringValue("id"));
            if (model != null)
            {

                YLCompanySelect1.ComPid = model.GongSiId;
                YLCompanySelect1.CompName = model.GongSiMingCheng;
                txtSPName.Text = model.MingCheng;//视频名称
                txtURL.Text = model.Filepath;//地址
                txtMiaoShu.Text = model.XiangXiJieShao;//详细介绍              
                ChuanZhiId = model.ChuanZhiId;
                XiLieId = model.XiLieId;

                MFileInfo file = new MFileInfo();
                file.FilePath = model.ShiPinIMG;
                var items = new List<MFileInfo>();
                items.Add(file);
                upload1.YuanFiles = items;
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

        /// <summary>
        /// 获取船只
        /// </summary>
        void getCZ()
        {
            int recordCount = 0;
            string comPid = Utils.GetQueryStringValue("gongsi");
            string xilieid = Utils.GetQueryStringValue("xilie");
            string selectID = Utils.GetQueryStringValue("sid");
            StringBuilder xilieStr = new StringBuilder();
            xilieStr.Append("<option value=\"\">请选择</option>");
            var chuanzhis = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhis(SiteUserInfo.CompanyId, 1000, 1, ref recordCount, new MChuanZhiChaXunInfo() { XiLieId = xilieid, });
            if (chuanzhis != null && chuanzhis.Count > 0)
            {
                for (int i = 0; i < chuanzhis.Count; i++)
                {
                    if (chuanzhis[i].ChuanZhiId == selectID)
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\"  selected=\"selected\">{1}</option> ", chuanzhis[i].ChuanZhiId, chuanzhis[i].MingCheng);
                    }
                    else
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\" >{1}</option>", chuanzhis[i].ChuanZhiId, chuanzhis[i].MingCheng);
                    }
                }

            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("1", "", xilieStr.ToString()));
            Response.End();
        }
    }
}
