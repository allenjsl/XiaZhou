using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Model.GysStructure;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.UserControl
{
    public partial class JiFenHotList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initData();
            }
        }

        protected void initData()
        {
            MWzJiFenShangPinChaXunInfo chaxun = new MWzJiFenShangPinChaXunInfo()
            {
                PaiXu = 0
            };

            var Commodel = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            int recordCount = 0;
            EyouSoft.BLL.YlStructure.BDuiHuan db = new EyouSoft.BLL.YlStructure.BDuiHuan();
            IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo> list = db.GetJiFenShangPins(Commodel.CompanyId, 10, 1, ref recordCount, chaxun);
            rptList.DataSource = list;
            rptList.DataBind();
        }
        protected string ImageView(object list)
        {
            string sb = "";
            if (list != null)
            {
                var model = (List<EyouSoft.Model.YlStructure.MFuJianInfo>)list;
                if (model != null && model.Count > 0)
                {
                    sb = TuPian.F1(Utils.GetErpFilepath() + model[0].Filepath, 79, 67);
                }
            }
            return sb;
        }
    }
}