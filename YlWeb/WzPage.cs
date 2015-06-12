using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Security.Membership;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb
{
    /// <summary>
    /// wz page
    /// </summary>
    public class WzPage : System.Web.UI.Page
    {
        /// <summary>
        /// 游轮网站会员信息业务实体
        /// </summary>
        protected MWzYuMingInfo YuMingInfo = null;
        /// <summary>
        /// ERP上传文件路径
        /// </summary>
        protected string ErpFilepath = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            MWzYuMingInfo m = null;
            m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();

            if (m == null) RCWE("ERROR:YUMING");
            if (string.IsNullOrEmpty(m.CompanyId)) RCWE("ERROR:YUMING");

            YuMingInfo = m;

            ErpFilepath = "http://" + YuMingInfo.ErpYuMing;
        }

        /// <summary>
        /// Response.Clear();Response.Write(s);Response.End();
        /// </summary>
        /// <param name="s">输出字符串</param>
        protected void RCWE(string s)
        {
            Response.Clear();
            Response.Write(s);
            Response.End();
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
