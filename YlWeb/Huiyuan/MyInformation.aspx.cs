using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class MyInformation : EyouSoft.YlWeb.HuiYuanPage
    {
        protected string XingBie = string.Empty;
        protected int GuoJia = 0;
        protected int ShengFen = 0;
        protected int ChengShi = 0;
        protected int XianQu = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                this.Save();
            }
        }

        void Save()
        {
            var m = this.GetForValue();
            var b = new BLL.YlStructure.BHuiYuan();
            var i = 0;

            i = b.UpdateHuiYuan(m);

            if (i > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功！")); else Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败！"));
        }

        /// <summary>
        /// 获取表单
        /// </summary>
        /// <returns></returns>
        Model.YlStructure.MHuiYuanInfo GetForValue()
        {
            var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(this.HuiYuanInfo.HuiYuanId);
            if (m != null)
            {
                m.XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(Utils.GetFormValue("ddlXingBie"));
                m.XingMing = Utils.GetFormValue(this.txtXingMing.UniqueID);
                m.GuoJi=Utils.GetFormValue(this.txtGuoJi.UniqueID);
                m.YouXiang=Utils.GetFormValue(this.txtYouXiang.UniqueID);
                m.ShengRi=Utils.GetDateTime(Utils.GetFormValue(txtShengRi.UniqueID));
                m.ShouJi = Utils.GetFormValue(this.txtShouJi.UniqueID);
                m.DianHua =Utils.GetFormValue(this.txtDianHua.UniqueID);
                m.GuoJiaId=Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID));
                m.ShengFenId=Utils.GetInt(Utils.GetFormValue(this.ddlProvice.UniqueID));
                m.ChengShiId=Utils.GetInt(Utils.GetFormValue(this.ddlCity.UniqueID));
                m.XianQuId=Utils.GetInt(Utils.GetFormValue(this.ddlCounty.UniqueID));
                m.DiZhi = Utils.GetFormValue(this.txtDiZhi.UniqueID);
            }
            return m;
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        void InitPage()
        {
            var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(this.HuiYuanInfo.HuiYuanId);
            if (m != null)
            {
                this.txtUsername.Value = m.Username;
                this.XingBie = ((int)m.XingBie).ToString();
                this.txtXingMing.Value = m.XingMing;
                this.txtGuoJi.Value = m.GuoJi;
                this.txtYouXiang.Value = m.YouXiang;
                this.txtShengRi.Value = m.ShengRi.ToString("yyyy-MM-dd");
                this.txtShouJi.Value = m.ShouJi;
                this.txtDianHua.Value = m.DianHua;
                this.GuoJia = m.GuoJiaId;
                this.ShengFen = m.ShengFenId;
                this.ChengShi = m.ChengShiId;
                this.XianQu = m.XianQuId;
                this.txtDiZhi.Value = m.DiZhi;
                this.txtGuoJi.Value = m.GuoJi;
            }
        }
    }
}
