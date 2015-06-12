using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControl
{
    /// <summary>
    /// 整点时间下拉框
    /// 创建人：李晓欢
    /// 创建时间:2011-09-25
    /// </summary>
    public partial class ddlTime : System.Web.UI.UserControl
    {
        //赋值
        private string _setValue;
        public string SetValue
        {
            get { return _setValue; }
            set { _setValue = value; }
        }

        //取值
        private string _getValue;
        public string GetValue
        {
            get { return _getValue; }
            set { _getValue = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateInitTimeHtml();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            GetTimeHtml();
            base.OnLoad(e);
        }

        //初始化数据
        protected void DateInitTimeHtml()
        {
            for (int i = 0; i < 24; i++)
            {
                ListItem item = new ListItem();
                item.Value = i.ToString();
                item.Text = i + ":00";
                this.DropDownList1.Items.Add(item);
                if (SetValue == i.ToString())
                {
                    this.DropDownList1.SelectedValue = SetValue;
                }
            }
        }

        //获取下拉框的值
        protected void GetTimeHtml()
        {
            DateTime time = EyouSoft.Common.Utils.GetDateTime(EyouSoft.Common.Utils.GetFormValue(this.DropDownList1.UniqueID));
            if (time.ToString() != "" || time.ToString() != null)
            {
                GetValue = time.ToString();
            }
        }
    }
}