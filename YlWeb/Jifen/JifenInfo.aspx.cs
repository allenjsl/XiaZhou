using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.YlStructure;
using System.Text;

namespace EyouSoft.YlWeb.Jifen
{
    public partial class JifenInfo : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IsXianShiHengFu = false;
            
            if (!Page.IsPostBack)
            {
                initData(Utils.GetQueryStringValue("id"));
            }
        }
        protected void initData(string id)
        {
            if (string.IsNullOrEmpty(id)) Response.Redirect("jifenlist.aspx");

            MYlHuiYuanInfo userInfo = null;
            bool IsLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out userInfo);
            if (IsLogin)
            {
                var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(userInfo.HuiYuanId);
                phlLoginY.Visible = false;
                phLoginIn.Visible = true;
                ltr_jfNumber.Text = huiYuanInfo.KeYongJiFen.ToString("0.00");
                //ltr_jfCar.Text = "";
            }
            var model = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenShangPinInfo(id);

            if (model == null) Response.Redirect("jifenlist.aspx");

            if (model != null)
            {
                span_Shenyu.InnerText = model.ShengYuShuLiang.ToString();
                span_JfName.InnerText = model.MingCheng;
                ltr_function.Text = DuiHuanStr(model.FangShis);
                span_PS.InnerText = model.PeiSongFangShi;
                //span_Shenyu.InnerText=model.s
                ltr_shuoming.Text = model.ShuoMing;
                ltr_duihuan.Text = model.XuZhi;
                if (model.FuJians != null && model.FuJians.Count > 0)
                {
                    rptList1.DataSource = model.FuJians;
                    rptList1.DataBind();
                    rptList2.DataSource = model.FuJians;
                    rptList2.DataBind();
                }
            }
        }

        #region 兑换方式
        protected string DuiHuanStr(IList<MWzJiFenShangPinFangShiInfo> model)
        {
            StringBuilder sb = new StringBuilder();
            
            if (model != null&&model.Count>0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    string className="class=''";
                    if (i == 0)
                    {
                        className = "class=\"card_select\"";
                    }
                    sb.AppendFormat("<li><a "+className+" data-id=\"{1}\" data-JiFen=\"{2}\" data-JinE=\"{3}\" data-shuliang=\"\" href=\"javascript:void(0)\">{0}<span></span></a></li>",
                        model[i].FangShi.ToString(), (int)model[i].FangShi, model[i].JiFen.ToString("F0"), model[i].JinE.ToString("F2"));
                    className = "";
                }
            }
            return sb.ToString();
        }

        protected string ImageView(object index, object pic)
        {
            StringBuilder sb = new StringBuilder();
            if (index != null && pic != null)
            {
                string left = "";
                if (index.ToString() == "1")
                {
                    left = "left: 1113px";
                }
                else if (index.ToString() == "2")
                {
                    left = "left: 371px;";
                }
                else if (index.ToString() == "3")
                {
                    left = "left: 742px;";
                }
                sb.AppendFormat("<li style=\"position: absolute; {0}; display: block;\"><a href=\"{2}\" target=\"_blank\"><img src=\"{1}\"></a></li>",
                        left, TuPian.F1(ErpFilepath + pic.ToString(), 371, 273), ErpFilepath + pic.ToString());
            }
            return sb.ToString();
        }
        #endregion
    }
}
