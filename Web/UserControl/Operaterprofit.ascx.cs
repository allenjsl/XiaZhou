using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControl
{
    /// <summary>
    /// 计调中心-返利用户控件
    /// 创建人:lixh
    /// 创建时间:2012-03-19
    /// </summary>
    public partial class Operaterprofit : System.Web.UI.UserControl
    {
        /// <summary>
        /// 赋值
        /// </summary>
        private bool _SetValue;
        public bool SetValue
        {
            get { return _SetValue; }
            set { _SetValue = value; }
        }

        /// <summary>
        /// 取值
        /// </summary>
        private bool _GetValue;
        public bool GetValue
        {
            get { return _GetValue; }
            set { _GetValue = value; }
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        private bool _IsMust;
        public bool IsMust
        {
            get { return _IsMust; }
            set { _IsMust = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetprofitValue();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                this.GetValue = this.ddlprofit.SelectedValue == "0" ? true : false;
            }
            base.OnLoad(e);
        }

        protected void GetprofitValue()
        {
            this.ddlprofit.Items.Clear();
            this.ddlprofit.Items.Add(new ListItem("请选择","-1"));
            this.ddlprofit.Items.Add(new ListItem("有", "0"));
            this.ddlprofit.Items.Add(new ListItem("无", "1"));
            this.ddlprofit.SelectedValue = this.SetValue == true ? "0" : "1";            
        }
    }
}
