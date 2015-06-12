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
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class YouKe : EyouSoft.YlWeb.HuiYuanPage
    {
        protected int pageSize = 10;
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitRpt();
            }
            if (Utils.GetQueryStringValue("del") == "1") Del();
        }

        void Del()
        {
            var ids = Utils.GetQueryStringValue("ids").Split(',');
            var msg = string.Empty;
            var result = 0;
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    result = new EyouSoft.BLL.YlStructure.BHuiYuan().DeleteChangLvKe(this.HuiYuanInfo.CompanyId, this.HuiYuanInfo.HuiYuanId, id);
                }
            }
            if (result == 1)
            {
                msg = UtilsCommons.AjaxReturnJson("1", "删除成功");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "删除失败");
            }
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }

        void InitRpt()
        {
            var l = new BLL.YlStructure.BHuiYuan().GetChangLvKes(this.HuiYuanInfo.CompanyId, pageSize, pageIndex, ref recordCount, this.GetChaXun());

            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }
            else
            {
                this.phdNoDat.Visible = true;
            }
        }

        MHuiYuanChangLvKeChaXunInfo GetChaXun()
        {
            var m = new MHuiYuanChangLvKeChaXunInfo()
            {
                HuiYuanId = this.HuiYuanInfo.HuiYuanId
            };
            return m;
        }
    }
}
