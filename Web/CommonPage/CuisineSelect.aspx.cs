using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.Web.CommonPage
{
    public partial class CuisineSelect : System.Web.UI.Page
    {
        protected int listCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                this.DataInit();
            }
        }

        private void DataInit()
        {
            List<EyouSoft.Common.EnumObj> lst = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.SourceCuisine));
            this.RepList.DataSource = lst.Where(e => e.Text != EyouSoft.Model.EnumType.SourceStructure.SourceCuisine.未选择.ToString());
            this.RepList.DataBind();
            listCount = lst.Count();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
    }
}
