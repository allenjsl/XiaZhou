using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.YlWeb
{
    /// <summary>
    /// huiyuan page
    /// </summary>
    public class HuiYuanPage : System.Web.UI.Page
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected MYlHuiYuanInfo HuiYuanInfo = null;
        protected decimal KeYongJiFen = 0;
        protected int ShouCangShu = 0;
        protected string TuXiang = string.Empty;
        protected int DaiFuKuanDingDanShu = 0;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            MYlHuiYuanInfo m = null;
            bool isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            if (!isLogin)
            {
                Response.Redirect("/login.aspx?rurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(m.HuiYuanId);
            if (huiYuanInfo != null)
            {
                
                var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

                KeYongJiFen = huiYuanInfo.KeYongJiFen;
                ShouCangShu = huiYuanInfo.ShouCangShu;
                TuXiang = string.IsNullOrEmpty(huiYuanInfo.TuXiang) ? "/images/default-head.jpg" : TuPian.F1("http://" + yuMingInfo.YuMing + huiYuanInfo.TuXiang, 104, 100);
                DaiFuKuanDingDanShu = huiYuanInfo.DaiFuKuanDingDanShu;
            }

            HuiYuanInfo = m;
        }

        /// <summary>
        /// register scripts
        /// </summary>
        /// <param name="script"></param>
        protected void RegisterScript(string script)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
