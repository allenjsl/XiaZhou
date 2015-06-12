using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class YouKeEdit : EyouSoft.YlWeb.HuiYuanPage
    {
        protected string LeiXing = string.Empty;
        protected string ZhuangTai = string.Empty;
        protected string ZhengJianLeiXing = string.Empty;
        protected int GuoJia = 0;
        protected int ShengFen = 0;
        protected int ChengShi = 0;
        protected int XianQu = 0;
        protected int GuoJi = 0;
        protected string XingBie = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitGuoJi();
                this.InitPage();
            }
            if(Utils.GetQueryStringValue("dotype")=="save"){
                this.Save();
            }
        }

        void Save() {
            var m = this.GetForValue();
            var b = new BLL.YlStructure.BHuiYuan();
            var i = 0;

            if (string.IsNullOrEmpty(Utils.GetQueryStringValue("id")))
            {
                i=b.InsertChangLvKe(m);
            }
            else
            {
                i=b.UpdateChangLvKe(m);
            }

            if (i > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1","保存成功！")); else Utils.RCWE(UtilsCommons.AjaxReturnJson("0","保存失败！"));
        }

        /// <summary>
        /// 获取表单
        /// </summary>
        /// <returns></returns>
        Model.YlStructure.MHuiYuanChangLvKeInfo GetForValue()
        {
            return new EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo() { 
            HuiYuanId=this.HuiYuanInfo.HuiYuanId,
            LvkeId=Utils.GetQueryStringValue("id"),
            XingMing=Utils.GetFormValue(this.txtXingMing.UniqueID),
            LeiXing = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(Utils.GetFormValue("ddlLeiXing")),
            ZhengJianLeiXing = (EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)Utils.GetInt(Utils.GetFormValue("ddlZhengJianLeiXing")),
            ZhengJianHaoMa = Utils.GetFormValue(this.txtZhengJianHaoMa.UniqueID),
            ZhengJianYouXiaoQi = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtZhengJianYouXiaoQi.UniqueID)),
            ChuShengRiQi = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtChuShengRiQi.UniqueID)),
            DianHua = Utils.GetFormValue(this.txtDianHua.UniqueID),
            ShouJi = Utils.GetFormValue(this.txtShouJi.UniqueID),
            GuoJiaId = Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID)),
            ShengFenId = Utils.GetInt(Utils.GetFormValue(this.ddlProvice.UniqueID)),
            ChengShiId=Utils.GetInt(Utils.GetFormValue(this.ddlCity.UniqueID)),
            XianQuId=Utils.GetInt(Utils.GetFormValue(this.ddlCounty.UniqueID)),
            ZhuangTai = (EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus)Utils.GetInt(Utils.GetFormValue("ddlZhuangTai")),
            GuoJiId=Utils.GetInt(Utils.GetFormValue(this.ddlGuoJi.UniqueID)),
            GuoJi=this.ddlGuoJi.SelectedItem.Text,
            XingBie = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(Utils.GetFormValue("ddlXingBie")),
            IssueTime=DateTime.Now
            };
        }

        /// <summary>
        /// 国籍初始化
        /// </summary>
        void InitGuoJi()
        {
            var l = new BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(this.HuiYuanInfo.CompanyId, new Model.YlStructure.MJiChuXinXiChaXunInfo() { LeiXing = Model.EnumType.YlStructure.JiChuXinXiLeiXing.国籍 });
            this.ddlGuoJi.DataTextField = "MingCheng";
            this.ddlGuoJi.DataValueField = "XinXiId";
            this.ddlGuoJi.DataSource = l; this.ddlGuoJi.DataBind();
            this.ddlGuoJi.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        void InitPage()
        {
            var m = new BLL.YlStructure.BHuiYuan().GetChangLvKeInfo(Utils.GetQueryStringValue("id"));
            if (m != null)
            {
                this.txtXingMing.Value = m.XingMing;
                this.LeiXing = ((int)m.LeiXing).ToString();
                this.ZhengJianLeiXing = ((int)m.ZhengJianLeiXing).ToString();
                this.txtZhengJianHaoMa.Value = m.ZhengJianHaoMa;
                this.txtZhengJianYouXiaoQi.Value = m.ZhengJianYouXiaoQi.HasValue ? m.ZhengJianYouXiaoQi.Value.ToShortDateString() : string.Empty;
                this.txtChuShengRiQi.Value=m.ChuShengRiQi.HasValue?m.ChuShengRiQi.Value.ToShortDateString():string.Empty;
                this.txtDianHua.Value = m.DianHua;
                this.txtShouJi.Value = m.ShouJi;
                this.ddlCountry.SelectedValue = m.GuoJiaId.ToString();
                this.GuoJia = m.GuoJiaId;
                this.ddlProvice.SelectedValue = m.ShengFenId.ToString();
                this.ShengFen = m.ShengFenId;
                this.ddlCity.SelectedValue = m.ChengShiId.ToString();
                this.ChengShi = m.ChengShiId;
                this.ddlCounty.SelectedValue = m.XianQuId.ToString();
                this.XianQu = m.XianQuId;
                this.ZhuangTai = ((int)m.ZhuangTai).ToString();
                this.ddlGuoJi.SelectedValue = m.GuoJiId.ToString();
                this.XingBie = ((int)m.XingBie).ToString();
            }
        }
    }
}
