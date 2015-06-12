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
    public partial class LiPinKa : EyouSoft.YlWeb.HuiYuanPage
    {
        protected int pageSize = 10;
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitRpt();
        }

        void InitRpt()
        {
            var l = new BLL.YlStructure.BLiPinKa().GetLiPinKaDingDans(this.HuiYuanInfo.CompanyId, pageSize, pageIndex, ref recordCount, this.GetChaXun());

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

        MLiPinKaDingDanChaXunInfo GetChaXun()
        {
            var m = new MLiPinKaDingDanChaXunInfo()
            {
                HuiYuanId=this.HuiYuanInfo.HuiYuanId,
            };
            return m;
        }
    }
}
