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
    public partial class NavLeft : System.Web.UI.UserControl
    {
        MWzYuMingInfo YuMingInfo = null;
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? YouLunLeiXing { get; set; }
        protected string S1 = "游轮";
        protected string S2 = "登船地址";

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

            if (YouLunLeiXing.HasValue)
            {
                if (YouLunLeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                {
                    S1 = "邮轮";
                    S2 = "出发港口";

                    phgs.Visible = true;
                    phxl.Visible = false;

                    YouLunGongSi();
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
                for (int i = 0; i < 3; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/HangQiList.aspx?hx={0}\">{1}</a>", list[i].XinXiId, list[i].MingCheng);
                    }
                }
                txtHangXian.Text = sb.ToString();
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
                for (int i = 0; i < 3; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/HangQiList.aspx?xl={0}\">{1}</a>", list[i].XiLieId, list[i].MingCheng);
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
                for (int i = 0; i < 3; i++)
                {
                    if (list.Count > i && list[i] != null)
                    {
                        sb.AppendFormat("<a href=\"/Hangqi/HangQiList.aspx?dz={0}\">{1}</a>", list[i].XinXiId, list[i].MingCheng);
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
                if (i < 3)
                {
                    sb.AppendFormat("<a href=\"/Hangqi/HangQiList.aspx?cfy={0}\">{1}</a>",time.AddMonths(i).ToString("yyyy-MM"), time.AddMonths(i).ToString("Y"));
                }
                sb1.AppendFormat("<li><a href=\"/Hangqi/HangQiList.aspx?cfy={0}\">{1}</a></li>", time.AddMonths(i).ToString("yyyy-MM"), time.AddMonths(i).ToString("Y"));
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
                        sb.AppendFormat("<a href=\"/Hangqi/HangQiList.aspx?gs={0}\">{1}</a>", list[i].GongSiId, list[i].MingCheng);
                    }
                }
                txtGongSi.Text = sb.ToString();
                rptList_GongSi.DataSource = list;
                rptList_GongSi.DataBind();
            }
        }
    }
}