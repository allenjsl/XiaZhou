using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Corp
{
    public partial class ZiXunXX : WzPage
    {
        protected int pageIndex = 1;
        protected int pageSize = 30;
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? _LeiXing = null;
            EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing? _LeiXing1 = null;
            var info = new EyouSoft.BLL.YlStructure.BWz().GetZiXunInfo(Utils.GetQueryStringValue("s"));

            if (info != null)
            {
                ltr.Text = info.LeiXing.ToString();
                ltr0.Text = info.BiaoTi;
                Title = info.BiaoTi;
                ltr1.Text = "发布时间：" + info.IssueTime.ToString("yyyy-MM-dd");
                ltr2.Text = info.NeiRong;

                _LeiXing1 = info.LeiXing;
            }
            else if (Enum.IsDefined(typeof(Model.EnumType.YlStructure.WzZiXunLeiXing), Utils.GetInt(Utils.GetQueryStringValue("t"))))
            {
                _LeiXing1 = (Model.EnumType.YlStructure.WzZiXunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("t"));
            }

            switch (_LeiXing1)
            {
                case EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮攻略:
                case EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答:
                    _LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
                    break;
                case EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮攻略:
                case EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮问题解答:
                    _LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮;
                    break;
            }


            if (_LeiXing1 == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答 || _LeiXing1 == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮问题解答)
            {
                ltr.Text = "问题解答";
                ltr1.Visible = false;
            }

            if (_LeiXing1 == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮攻略)
            {
                ltr.Text = "游轮攻略";
                ltr1.Visible = false;
            }

            if (_LeiXing1 == EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.海洋邮轮攻略)
            {
                ltr.Text = "邮轮攻略";
                ltr1.Visible = false;
            }

            int recordCount = 0;
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = new EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo();
            chaXun.LeiXing = _LeiXing1;
            var items = new EyouSoft.BLL.YlStructure.BWz().GetZiXuns(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);
            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }

            var chaxun = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo()
            {
                IsYouXiao = true,
                LeiXing = _LeiXing,
                BiaoQian = EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门推荐
            };
            var list = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(YuMingInfo.CompanyId, 3, 1, ref recordCount, chaxun);
            if (list != null)
            {
                rptHot.DataSource = list;
                rptHot.DataBind();
            }
        }

        protected string GetHQURL(object leixing, object hangqiid)
        {
            return ((int)leixing == (int)EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 ? "/hangqi" : "/hangqi/HY") + hangqiid + ".html";
        }

        protected string GetTu(object fujians)
        {
            var s = string.Empty;
            var l = (IList < EyouSoft.Model.YlStructure.MFuJianInfo >) fujians;
            if (l != null && l.Count > 0)
            {
                s = EyouSoft.YlWeb.TuPian.F1(Utils.GetErpFilepath() + l[0].Filepath, 232, 167);
            }
            return s;
        }
    }
}
