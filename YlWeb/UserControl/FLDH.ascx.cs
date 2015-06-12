using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using System.Text;

namespace EyouSoft.YlWeb.UserControl
{
    public partial class FLDH : System.Web.UI.UserControl
    {
        MWzYuMingInfo YuMingInfo = null;
        EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? _YouLunLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;

        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? YouLunLeiXing
        {
            get
            {
                if (!_YouLunLeiXing.HasValue) return EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
                return _YouLunLeiXing;
            }
            set { _YouLunLeiXing = value; }
        }

        /// <summary>
        /// 是否首页
        /// </summary>
        public bool IsIndex { get; set; }
        protected string S1 = "游轮";
        protected string S2 = "登船地址";
        protected string MenuClass = string.Empty;
        protected string YouHuiLianJie = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MWzYuMingInfo m = null;
                m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
                if (m != null)
                {
                    YuMingInfo = m;
                }
                YouLunHangXiang(); YouLunXiLie(); DengChuan(); ChuFaTime();
            }

            Model.EnumType.YlStructure.WzYouQingLianJieLeiXing? _youhuileixing = Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.长江优惠信息;

            if (YouLunLeiXing.HasValue)
            {
                if (YouLunLeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                {
                    S1 = "邮轮";
                    S2 = "出发港口";
                    MenuClass = " sub-menu01";

                    phgs.Visible = true;
                    phxl.Visible = false;

                    YouLunGongSi();
                    _youhuileixing = Model.EnumType.YlStructure.WzYouQingLianJieLeiXing.海洋优惠信息;
                }
            }

            var l = new BLL.YlStructure.BWz().GetYouQingLianJies(YuMingInfo.CompanyId, new MWzYouQingLianJieChaXunInfo() { LeiXing = _youhuileixing });
            if (l != null && l.Count > 0)
            {
                this.YouHuiLianJie += "<p class=\"hy_red\">优惠信息</p>";
                foreach (var m in l)
                {
                    this.YouHuiLianJie += string.Format("<p><a target=\"_blank\" href=\"{0}\" class=\"hy_yellow\"><img src=\"../images/hy-icon01.gif\" />{1}</a></p>",m.Url,m.MingCheng);
                }
            }
        }

        /// <summary>
        /// 游轮航线
        /// </summary>
        private void YouLunHangXiang()
        {
            StringBuilder sb = new StringBuilder();
            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线, YouLunLeiXing = YouLunLeiXing });
            if (list != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/chaxun.aspx?lx={2}&hx={0}\">{1}</a>", list[i].XinXiId, list[i].MingCheng,(int)YouLunLeiXing);
                    }
                }
                txtHangXian.Text = sb.ToString();
                if (YouLunLeiXing == Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                    rptList_HangXian.DataSource = GetYouLunHangXian(list);
                else
                    rptList_HangXian.DataSource = list;
                rptList_HangXian.DataBind();
            }
        }

        /// <summary>
        /// 游轮系列
        /// </summary>
        private void YouLunXiLie()
        {
            int recordCount = 0;
            StringBuilder sb = new StringBuilder();
            var chaXun = new EyouSoft.Model.YlStructure.MXiLieChaXunInfo();
            chaXun.GongSiLeiXing = YouLunLeiXing;

            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(YuMingInfo.CompanyId, 1000, 1, ref recordCount, chaXun);
            if (list != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/chaxun.aspx?lx={2}&xl={0}\">{1}</a>", list[i].XiLieId, list[i].MingCheng, (int)YouLunLeiXing);
                    }
                }
                txtXiLie.Text = sb.ToString();
                rptList_Xilie.DataSource = list;
                rptList_Xilie.DataBind();
            }
        }

        /// <summary>
        /// 登船地址
        /// </summary>
        private void DengChuan()
        {
            StringBuilder sb = new StringBuilder();
            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口, YouLunLeiXing = YouLunLeiXing });
            if (list != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/chaxun.aspx?lx={2}&gk={0}\">{1}</a>", list[i].XinXiId, list[i].MingCheng, (int)YouLunLeiXing);
                    }
                }
                txtDiZhi.Text = sb.ToString();
                rptList_DiZhi.DataSource = list;
                rptList_DiZhi.DataBind();
            }
        }
        /// <summary>
        /// 出发时间
        /// </summary>
        private void ChuFaTime()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            DateTime time = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                if (i <= 3)
                {
                    sb.AppendFormat("<a href=\"/Hangqi/chaxun.aspx?lx={2}&yf={0}\">{1}</a>", time.AddMonths(i).ToString("yyyy-MM"), time.AddMonths(i).ToString("Y"),(int)YouLunLeiXing);
                }
                sb1.AppendFormat("<em><a href=\"/Hangqi/chaxun.aspx?lx={2}&yf={0}\">{1}</a></em>", time.AddMonths(i).ToString("yyyy-MM"), time.AddMonths(i).ToString("Y"), (int)YouLunLeiXing);
            }
            txtChuFa.Text = sb.ToString();
            txtChuFaLi.Text = sb1.ToString();
        }

        /// <summary>
        /// 游轮系列
        /// </summary>
        private void YouLunGongSi()
        {
            int recordCount = 0;
            StringBuilder sb = new StringBuilder();
            var chaXun = new EyouSoft.Model.YlStructure.MGongSiChaXunInfo();
            chaXun.GongSiLeiXing = YouLunLeiXing;

            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSis(YuMingInfo.CompanyId, 1000, 1, ref recordCount, chaXun);
            if (list != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/chaxun.aspx?lx={2}&gs={0}\">{1}</a>", list[i].GongSiId, list[i].MingCheng, (int)YouLunLeiXing);
                    }
                }
                txtGongSi.Text = sb.ToString();
                rptList_GongSi.DataSource = list;
                rptList_GongSi.DataBind();
            }
        }

        /// <summary>
        /// 航线重构
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private IList<MYouLunHangXian> GetYouLunHangXian(IList<EyouSoft.Model.YlStructure.MJiChuXinXiInfo> l)
        {
            var lst = new List<MYouLunHangXian>();
            if (l != null && l.Count > 0)
            {
                var b = new BLL.YlStructure.BJiChuXinXi();
                //var ls = l.Select(m => m.PXinXiId).Distinct().ToList();
                //if (ls != null && ls.Count > 0)
                //{
                //    foreach (int m in ls)
                //    {
                //        var mdl = b.GetJiChuXinXiInfo(m);
                //        lst.Add(new MYouLunHangXian() { BieMing = mdl != null && !string.IsNullOrEmpty(mdl.BieMing) ? mdl.BieMing : string.Empty, YouLunHangXians = l.Where(mm => mm.PXinXiId == m).ToList() });
                //    }
                //}
                var ls = b.GetJiChuXinXis(YuMingInfo.CompanyId, new MJiChuXinXiChaXunInfo() { LeiXing = Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口, YouLunLeiXing = YouLunLeiXing });
                if (ls != null && ls.Count > 0)
                {
                    foreach (var m in ls)
                    {
                        if(!string.IsNullOrEmpty(m.BieMing))
                        lst.Add(new MYouLunHangXian() { BieMing = m.BieMing, YouLunHangXians = l.Where(mm => mm.PXinXiId == m.XinXiId).ToList() });
                    }
                }
                ls = l.Where(mm => mm.PXinXiId == 0).ToList();
                if (ls != null && ls.Count > 0)
                    lst.Add(new MYouLunHangXian() { BieMing = string.Empty, YouLunHangXians = ls });
            }
            return lst;
        }

        /// <summary>
        /// 航线绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_HangXian_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                var phdHangXianCJ = (System.Web.UI.WebControls.PlaceHolder)e.Item.FindControl("phdHangXianCJ");
                var phdHangXianHY = (System.Web.UI.WebControls.PlaceHolder)e.Item.FindControl("phdHangXianHY");

                if (YouLunLeiXing == Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                {
                    phdHangXianCJ.Visible = true;
                    var rptHangXian = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("rptHangXian");
                    var YouLunHangXians = (IList<Model.YlStructure.MJiChuXinXiInfo>)DataBinder.Eval(e.Item.DataItem, "YouLunHangXians");

                    if (YouLunHangXians != null && YouLunHangXians.Count > 0)
                    {
                        rptHangXian.DataSource = YouLunHangXians;
                        rptHangXian.DataBind();
                    }
                }
                else
                {
                    phdHangXianHY.Visible = true;
                }
            }
        }

        /// <summary>
        /// 获取航线系列船只
        /// </summary>
        /// <param name="mingcheng"></param>
        /// <param name="xielieid"></param>
        /// <returns></returns>
        protected string GetChuanZhis(object mingcheng, object xielieid)
        {
            var s = new StringBuilder();
            var l = new BLL.YlStructure.BJiChuXinXi().GetChuanZhis(YuMingInfo.CompanyId, new MChuanZhiChaXunInfo() { XiLieId = xielieid.ToString() });
            if (l != null && l.Count > 0)
            {
                foreach (var m in l)
                {
                    s.AppendFormat("   <dd><a title=\"{2}\" href=\"/Hangqi/chaxun.aspx?lx={0}&xl={1}&cz={3}&cn={2}\">{2}</a></dd>", (int)YouLunLeiXing, xielieid, m.MingCheng,m.ChuanZhiId);
                }
            }

            return s.ToString();
        }
    }

    /// <summary>
    /// 游轮航线实体重构
    /// </summary>
    public class MYouLunHangXian : Model.YlStructure.MJiChuXinXiInfo
    {
        /// <summary>
        /// 游轮航线集合
        /// </summary>
        public IList<Model.YlStructure.MJiChuXinXiInfo> YouLunHangXians { get; set; }
    }
}