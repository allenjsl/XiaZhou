using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControl
{
    public partial class SysTop : System.Web.UI.UserControl
    {
        protected string str = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            str = HttpContext.Current.Request.Url.AbsolutePath;
            switch (str)
            {
                case "/SmsCenter/SendMessage.aspx":
                    {
                        str = "sendmessage";
                        break;
                    }
                case "/SmsCenter/SendHistory.aspx":
                    {
                        str = "sendhistory";
                        break;
                    }
                case "/SmsCenter/SmsList.aspx":
                    {
                        str = "smslist";
                        break;
                    }
                case "/SmsCenter/SmsSetting.aspx":
                    {
                        str = "smssetting";
                        break;
                    }
                case "/SmsCenter/AccountInfo.aspx":
                    {
                        str = "accountinfo";
                        break;
                    }
            }
        }
    }
}