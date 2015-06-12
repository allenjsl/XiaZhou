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
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.YlWeb.Corp
{
    public partial class YiJian : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IsXianShiHengFu = false;
            if (Common.Utils.GetQueryStringValue("dotype") == "submit") { this.Save(); }
        }
        void Save() 
        {
            var r = new BLL.YlStructure.BWz().AddWZYiJianFanKui(this.GetModel());
            if (r > 0) RCWE(Common.UtilsCommons.AjaxReturnJson("1", "保存成功！")); else RCWE(Common.UtilsCommons.AjaxReturnJson("0", "保存失败！"));
        }
        Model.YlStructure.MWzYiJianFanKuiInfo GetModel()
        {
            var src=Common.Utils.GetFormValue(this.upFiles.ClientHideID).Split('|');
            MYlHuiYuanInfo m = null;
            var isLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out m);

            return new EyouSoft.Model.YlStructure.MWzYiJianFanKuiInfo() { 
                YiJianId=Guid.NewGuid().ToString(),
                LeiXing = (Model.EnumType.YlStructure.YiJianFanKuiLeiXing)Common.Utils.GetInt(Common.Utils.GetFormValue("radio")),
                CompanyId=YuMingInfo.CompanyId,
                FilePath = src!=null&&src.Count()>1?"http://"+YuMingInfo.YuMing+src[1]:string.Empty,
                MiaoShu = Common.Utils.GetFormValue("textfield"),
                RemoteIP=Request.UserHostAddress,
                IssueTime=DateTime.Now,
                Client = new EyouSoft.Toolkit.BrowserInfo().ToJsonString(),
                OperatorId=isLogin?m.HuiYuanId:string.Empty
            };
        }
    }
}
