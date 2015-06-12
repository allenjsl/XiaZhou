using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    public partial class SelectPeoples : System.Web.UI.UserControl
    {
        /// </summary>
        /// 创建人：徐从栎
        /// 创建时间：2011-11-3
        /// 说明：选择人事
        /// <summary>
        /// <summary>
        /// 销售员ID
        /// </summary>
        private string _sellsID;

        public string SellsID
        {
            get { return _sellsID; }
            set { _sellsID = value; }
        }

        /// <summary>
        /// 销售员姓名
        /// </summary>
        private string _sellsName;

        public string SellsName
        {
            get { return _sellsName; }
            set { _sellsName = value; }
        }
        /// <summary>
        /// 设置标题
        /// </summary>
        private string _setTitle;

        public string SetTitle
        {
            get { return _setTitle; }
            set { _setTitle = value; }
        }
        /// <summary>
        /// 是否在顶级打开
        /// </summary>
        private bool _isParentOpen;

        public bool IsParentOpen
        {
            get { return _isParentOpen; }
            set { _isParentOpen = value; }
        }

        /// <summary>
        /// 先赋值的控件的boxyID
        /// </summary>
        private string _parentIframeID;

        public string ParentIframeID
        {
            get { return _parentIframeID; }
            set { _parentIframeID = value; }
        }
        /// <summary>
        /// 单选设置 1，多选设置为2;默认单选
        /// </summary>
        private int _sMode;

        public int SMode
        {
            get { return _sMode; }
            set { _sMode = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                this.SellsID = Utils.GetFormValue(this.ClientID + "_hideSellID");
                this.SellsName = Utils.GetFormValue(this.ClientID + "_txtSellName");
                base.OnLoad(e);
            }
        }
    }
}