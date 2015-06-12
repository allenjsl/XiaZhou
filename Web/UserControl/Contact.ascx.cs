using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    public partial class Contact : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置控件的数据源
        /// </summary>
        private IList<EyouSoft.Model.CrmStructure.MCrmLinkman> _setTravelList;

        public IList<EyouSoft.Model.CrmStructure.MCrmLinkman> SetTravelList
        {
            get { return _setTravelList; }
            set { _setTravelList = value; }
        }
        /// <summary>
        /// 获得控件内的数据
        /// </summary>
        private IList<EyouSoft.Model.CrmStructure.MCrmLinkman> _getTravelList;

        public IList<EyouSoft.Model.CrmStructure.MCrmLinkman> GetTravelList
        {
            get { return _getTravelList; }
            set { _getTravelList = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDataList();
            }
        }

        /// <summary>
        /// 页面初始化时绑定数据
        /// </summary>
        private void SetDataList()
        {
            if (this.SetTravelList != null && this.SetTravelList.Count > 0)
            {
                this.rptList.DataSource = this.SetTravelList;
                this.rptList.DataBind();
                this.ph_showorhide.Visible = false;
            }
            else
            {
                this.ph_showorhide.Visible = true;
            }

        }
    }
}