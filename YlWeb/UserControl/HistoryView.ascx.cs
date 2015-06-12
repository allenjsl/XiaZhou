using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.UserControl
{
    public partial class HistoryView : System.Web.UI.UserControl
    {

        EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing _LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;

        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing
        {
            get { return _LeiXing; }
            set { _LeiXing = value; }
        }

        private bool isTuanGou = false;

        public bool IsTuanGou
        {
            get { return isTuanGou; }
            set { isTuanGou = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            MWzYuMingInfo m = null;
            m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            int recordCount = 0;
            StringBuilder sb = new StringBuilder();
            if (!IsTuanGou)
            {
                MHangQiChaXunInfo chaxun = new MHangQiChaXunInfo()
                {
                    IsYouXiao = true,

                    BiaoQian = EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门推荐
                };
                chaxun.LeiXing = LeiXing;

                var list = bll.GetHangQis(m.CompanyId, 7, 1, ref recordCount, chaxun);

                if (list != null)
                {
                    foreach (var item in list)
                    {

                        string FuJians = "";

                        string url = "/hangqi/";
                        var _leiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)item.LeiXing;
                        if (_leiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                            url = "/hangqi/HY";



                        if (item.FuJians != null && item.FuJians.Count > 0)
                            FuJians = TuPian.F1(Utils.GetErpFilepath() + item.FuJians[0].Filepath, 153, 104);
                        sb.AppendFormat("<li title='{0}'>", item.MingCheng);
                        sb.AppendFormat("<a href=\"{0}\"><img src=\"{1}\"><span style='height:23px;overflow:hidden;'>{2}", url + item.HangQiId + ".html", FuJians, item.MingCheng);
                        sb.Append("</span></a></li>");
                    }
                    litList.Text = sb.ToString();
                }
            }
            else
            {

                MTuanGouChaXunInfo tg = new MTuanGouChaXunInfo();

                var TuanGou = bll.GetTuanGous(m.CompanyId, 7, 1, ref recordCount, tg);

                if (TuanGou != null)
                {
                    foreach (var item in TuanGou)
                    {
                        string FuJians = "";

                        string url = "/TuanGou/TuanGouXiangQing.aspx";

                        FuJians = TuPian.F1(Utils.GetErpFilepath() + item.FengMian,153,104);
                        sb.AppendFormat("<li title='{0}'>", item.MingCheng);
                        sb.AppendFormat("<a href=\"{0}\"><img src=\"{1}\"><span style='height:23px;overflow:hidden;'>{2}", url + "?TuanGouId=" + item.TuanGouId, FuJians, item.MingCheng);
                        sb.Append("</span></a></li>");
                    }
                    
                }
            }
            litList.Text = sb.ToString();
        }
    }
}